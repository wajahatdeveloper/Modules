using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CloudServices;
using Firebase.Database;
using Microsoft.CSharp.RuntimeBinder;
using UnityEngine;

namespace DataLayer
{
    public partial class NetworkData
    {
        private string dateTimeFormat = "dd/MM/yyyy hh:mm:ss";

        [Serializable]
        public class UserEntry
        {
            public string userId;
            public string nickName;
            public string countryCode;
            public DateTime date;
            public int score;
        }

        public List<UserEntry> userEntries = new();

         public IEnumerator Routine_FetchAllUserEntriesByScore(Action onDone)
         {
             userEntries.Clear();

             var DBTask = FirebaseSDK.Instance.firebaseDb.Root.Child("users").OrderByChild("Score").GetValueAsync();
             yield return new WaitUntil(() => DBTask.IsCompleted);
             if (DBTask.Exception != null)
             {
                 Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
             }
             else
             {
                 //Data has been retrieved
                 DataSnapshot snapshot = DBTask.Result;
                 foreach (DataSnapshot dataSnapshot in snapshot.Children.Reverse())
                 {
                     if (dataSnapshot == null) continue;

                     UserEntry userEntry = null;

                     try
                     {
                         var date = DateTime.ParseExact(dataSnapshot.Child("Date").Value.ToString(), dateTimeFormat,
                             CultureInfo.InvariantCulture);
                         var userId = dataSnapshot.Child("userId").Value.ToString();
                         var nickName = dataSnapshot.Child("NickName").Value.ToString();
                         var score = int.Parse(dataSnapshot.Child("Score").Value.ToString());
                         var countryCode = dataSnapshot.Child("CountryCode").Value.ToString();
                         userEntry = new UserEntry
                         {
                             date = date, userId = userId,
                             nickName = nickName, score = score,
                             countryCode = countryCode
                         };
                     }
                     catch (NullReferenceException)
                     {
                         continue;
                     }

                     userEntries.Add(userEntry);
                 }
                 onDone.Invoke();
             }
         }

        public async Task<object> FetchFullUserEntryAsync(string playerUserId)
        {
            DebugX.Log($"{LogClassName} : Fetch Player Data for User Id {playerUserId}..", LogFilters.State,
                gameObject);

            if (string.IsNullOrEmpty(playerUserId))
            {
                DebugX.Log($"{LogClassName} : User Id must not be Empty", LogFilters.State, gameObject);
                return null;
            }

            // Fetch Player State from DB
            Query query = FirebaseSDK.Instance.firebaseDb
                .Child("users")
                .Child(playerUserId);

            var tcs = new TaskCompletionSource<object>();

            await FirebaseSDK.Instance.Async_FetchData(query,
                snapshot =>
                {
                    DebugX.Log($"{LogClassName} : Player Data Fetched Successfully.", LogFilters.State, gameObject);
                    tcs.SetResult(snapshot.Value);

                    string isInMatchStr = (string)snapshot.Child("IsInMatch").Value;
                    if (isInMatchStr.IsNotNullOrEmpty())
                    {
                        MirroredData.Instance.SetIsInMatch(bool.Parse(isInMatchStr));
                    }

                    string nickName = (string)snapshot.Child("NickName").Value;
                    if (nickName.IsNotNullOrEmpty())
                    {
                        if (MirroredData.Instance.GetPlayerNickName() == "")
                        {
                            MirroredData.Instance.SetPlayerNickName(nickName);
                        }
                    }

                    return Task.CompletedTask;
                },
                err =>
                {
                    DebugX.LogError($"{LogClassName} : Unable to Fetch Player Data. Reason: {err}", LogFilters.State,
                        gameObject);
                    tcs.SetResult(null);
                    return Task.CompletedTask;
                },
                tId =>
                {
                    tcs.SetResult(null);
                    return Task.CompletedTask;
                });

            await tcs.Task;

            return tcs.Task.Result;
        }

        internal async void UpdateToUserDatabaseEntryAsync(string key, object value)
        {
            string playerUserId = LocalData.Instance.GetPlayerUserId_Persistent();
            DebugX.Log($"{LogClassName} : Set Player Data Key:{key} Value:{value.ToString()}..", LogFilters.State,
                gameObject);

            if (string.IsNullOrEmpty(playerUserId))
            {
                DebugX.LogError($"{LogClassName} : Player Id Should not be null", LogFilters.State, null);
                return;
            }

            Query queryPath = FirebaseSDK.Instance.firebaseDb
                .Child("users")
                .Child(playerUserId)
                .Child(key);

            await FirebaseSDK.Instance.Async_SetData(queryPath, value.ToString(),
                tId =>
                {
                    DebugX.Log($"{LogClassName} : Player Data Set Successfully. Id:{tId}", LogFilters.State,
                        gameObject);
                    return Task.CompletedTask;
                },
                (tId, err) =>
                {
                    DebugX.LogError($"{LogClassName} : Unable to Set Player Data. Id:{tId} Reason: {err}",
                        LogFilters.State,
                        gameObject);
                    return Task.CompletedTask;
                });
        }

        internal async Task<object> FetchFromUserDatabaseEntryAsync(string key, object defaultValue)
        {
            string playerUserId = LocalData.Instance.GetPlayerUserId_Persistent();
            DebugX.Log($"{LogClassName} : Fetch Player Data Key: {key}..", LogFilters.State, gameObject);

            if (string.IsNullOrEmpty(playerUserId))
            {
                DebugX.LogError($"{LogClassName} : Player Id Should not be null", LogFilters.State, null);
                return null;
            }

            // Fetch Player State from DB
            Query query = FirebaseSDK.Instance.firebaseDb
                .Child("users")
                .Child(playerUserId)
                .Child(key);

            var tcs = new TaskCompletionSource<object>();

            await FirebaseSDK.Instance.Async_FetchData(query,
                snapshot =>
                {
                    DebugX.Log($"{LogClassName} : Player Data Fetched Successfully.", LogFilters.State, gameObject);
                    tcs.SetResult(snapshot.Value);
                    return Task.CompletedTask;
                },
                err =>
                {
                    DebugX.LogError($"{LogClassName} : Unable to Fetch Player Data. Reason: {err}", LogFilters.State,
                        gameObject);
                    tcs.SetResult(null);
                    return Task.CompletedTask;
                },
                tId =>
                {
                    tcs.SetResult(defaultValue);
                    return Task.CompletedTask;
                });

            await tcs.Task;

            return tcs.Task.Result;
        }

        public async void SendUserScoreToDatabaseAsync(string playerUserId, int value)
        {
            if (string.IsNullOrEmpty(playerUserId))
            {
                DebugX.LogError($"{LogClassName} : Player Id Should not be null", LogFilters.State, null);
                return;
            }

            DebugX.Log($"{LogClassName} : Set Player Score to Value:{value.ToString()}..", LogFilters.State,
                gameObject);

            Query queryPath = FirebaseSDK.Instance.firebaseDb
                .Child("users")
                .Child(playerUserId);

            var dictionary = new Dictionary<string, object>()
            {
                { "Score", value },
                { "Date", DateTime.Now.ToString(dateTimeFormat) },
            };

            await FirebaseSDK.Instance.Async_UpdateChildren(queryPath, dictionary,
                tId =>
                {
                    DebugX.Log($"{LogClassName} : Score for Player {playerUserId} Set Successfully. tId:{tId}",
                        LogFilters.State, gameObject);
                    return Task.CompletedTask;
                },
                (tId, err) =>
                {
                    DebugX.LogError(
                        $"{LogClassName} : Unable to Set Score for Player {playerUserId}. tId:{tId} Reason: {err}",
                        LogFilters.State,
                        gameObject);
                    return Task.CompletedTask;
                });
        }

        public async Task<(int, DateTime)> FetchUserScoreFromDatabaseAsync(string playerUserId)
        {
            var failState = (0, DateTime.UnixEpoch);

            if (string.IsNullOrEmpty(playerUserId))
            {
                DebugX.LogError($"{LogClassName} : Player Id Should not be null", LogFilters.State, null);
                return failState;
            }

            DebugX.Log($"{LogClassName} : Fetch Score for Player {playerUserId}..", LogFilters.State, gameObject);

            // Fetch Player State from DB
            Query query = FirebaseSDK.Instance.firebaseDb
                .Child("users")
                .Child(playerUserId);

            var tcs = new TaskCompletionSource<(int, DateTime)>();

            await FirebaseSDK.Instance.Async_FetchData(query,
                snapshot =>
                {
                    DebugX.Log(
                        $"{LogClassName} : Score for Player {playerUserId}, Fetched Successfully, Value={snapshot.Value}.",
                        LogFilters.State, gameObject);

                    dynamic jsonObject = SimpleJSON.JSON.Parse(snapshot.Value.ToString());

                    try
                    {
                        int score = jsonObject.Score;
                        DateTime datetime = DateTime.ParseExact(jsonObject.Date.ToString(), dateTimeFormat, CultureInfo.InvariantCulture);
                        tcs.SetResult((score, datetime));
                    }
                    catch (RuntimeBinderException)
                    {
                        tcs.SetResult(failState);
                    }

                    return Task.CompletedTask;
                },
                err =>
                {
                    DebugX.LogError($"{LogClassName} : Unable to Fetch Score for Player {playerUserId}. Reason: {err}",
                        LogFilters.State,
                        gameObject);
                    tcs.SetResult(failState);
                    return Task.CompletedTask;
                },
                tId =>
                {
                    tcs.SetResult(failState);
                    return Task.CompletedTask;
                });

            await tcs.Task;

            return tcs.Task.Result;
        }

        public async void SendUserCountryCodeToDatabaseAsync(string playerUserId, string countryCode)
        {
            if (string.IsNullOrEmpty(playerUserId))
            {
                DebugX.LogError($"{LogClassName} : Player Id Should not be null", LogFilters.State, null);
                return;
            }

            Query queryPath = FirebaseSDK.Instance.firebaseDb
                .Child("users")
                .Child(playerUserId);

            var dictionary = new Dictionary<string, object>()
            {
                { "CountryCode", countryCode },
            };

            await FirebaseSDK.Instance.Async_UpdateChildren(queryPath, dictionary,
                tId =>
                {
                    DebugX.Log($"{LogClassName} : Score for Player {playerUserId} Set Successfully. tId:{tId}",
                        LogFilters.State, gameObject);
                    return Task.CompletedTask;
                },
                (tId, err) =>
                {
                    DebugX.LogError(
                        $"{LogClassName} : Unable to Set Score for Player {playerUserId}. tId:{tId} Reason: {err}",
                        LogFilters.State,
                        gameObject);
                    return Task.CompletedTask;
                });
        }

        public async Task<string> FetchUserCountryCodeFromDatabaseAsync(string playerUserId)
        {
            var failState = "UN";

            if (string.IsNullOrEmpty(playerUserId))
            {
                DebugX.LogError($"{LogClassName} : Player Id Should not be null", LogFilters.State, null);
                return failState;
            }

            // Fetch Player State from DB
            Query query = FirebaseSDK.Instance.firebaseDb
                .Child("users")
                .Child(playerUserId);

            var tcs = new TaskCompletionSource<string>();

            await FirebaseSDK.Instance.Async_FetchData(query,
                snapshot =>
                {
                    DebugX.Log(
                        $"{LogClassName} : Score for Player {playerUserId}, Fetched Successfully, Value={snapshot.Value}.",
                        LogFilters.State, gameObject);

                    dynamic jsonObject = SimpleJSON.JSON.Parse(snapshot.Value.ToString());

                    try
                    {
                        string countryCode = jsonObject.CountryCode;
                        tcs.SetResult(countryCode);
                    }
                    catch (RuntimeBinderException)
                    {
                        tcs.SetResult(failState);
                    }

                    return Task.CompletedTask;
                },
                err =>
                {
                    DebugX.LogError($"{LogClassName} : Unable to Fetch Score for Player {playerUserId}. Reason: {err}",
                        LogFilters.State,
                        gameObject);
                    tcs.SetResult(failState);
                    return Task.CompletedTask;
                },
                tId =>
                {
                    tcs.SetResult(failState);
                    return Task.CompletedTask;
                });

            await tcs.Task;

            return tcs.Task.Result;
        }
    }
}