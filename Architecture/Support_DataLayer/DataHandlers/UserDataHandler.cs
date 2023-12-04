using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

namespace DataLayer
{
    public class UserDataHandler : SingletonBehaviour<UserDataHandler>
    {
        private const string LogClassName = "UserDataHandler";

        public async Task<object> FetchAllUserData(string playerUserId)
        {
            DebugX.Log($"{LogClassName} : Fetch Player Data for User Id {playerUserId}..", LogFilters.State, gameObject);

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
                        MirroredData.Instance.SetPlayerNickName(nickName);
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

        internal async void SendToDatabaseAsync(string key, object value)
        {
            string playerUserId = LocalData.Instance.GetPlayerUserId_Persistent();
            DebugX.Log($"{LogClassName} : Set Player Data Key:{key} Value:{value.ToString()}..", LogFilters.State, gameObject);

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
                    DebugX.Log($"{LogClassName} : Player Data Set Successfully. Id:{tId}", LogFilters.State, gameObject);
                    return Task.CompletedTask;
                },
                (tId, err) =>
                {
                    DebugX.LogError($"{LogClassName} : Unable to Set Player Data. Id:{tId} Reason: {err}", LogFilters.State,
                        gameObject);
                    return Task.CompletedTask;
                });
        }

        internal async Task<object> FetchFromDatabase(string key, object defaultValue)
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
    }
}