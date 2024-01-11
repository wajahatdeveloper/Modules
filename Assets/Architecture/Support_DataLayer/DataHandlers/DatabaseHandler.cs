using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudServices;
using Firebase.Database;
using UnityEngine;

namespace DataLayer
{
    public class DatabaseHandler : SingletonBehaviour<DatabaseHandler>
    {
        private const string LogClassName = "DatabaseHandler";

        internal async void SendToDatabaseAsync(string playerUserId, string key, object value)
        {
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

        internal async Task<object> FetchFromDatabaseAsync(string playerUserId, string key, object defaultValue)
        {
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