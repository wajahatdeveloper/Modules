using System;
using System.Threading.Tasks;
using CloudServices;
using Firebase.Database;

namespace DataLayer
{
    public partial class NetworkData
    {
        private string _roomKey = "rooms";
        private string _roomStateKey = "RoomState";

        public enum RoomKeyPlayerStatus
        {
            UnAvailable = 0,
            Left = 1,
            Disconnected = 2,
            Present = 3,
        }

        public enum RoomKeyStatus
        {
            InLobby = 0,
            InSession = 1,
            InConclusion = 2,
            Closed = 3,
        }

        public async void SetRoomPlayerStatus(string playerId, string roomId, RoomKeyPlayerStatus value)
        {
            DebugX.Log($"{LogClassName} : Set Room State for Player Id: {playerId} with Value: {value}..",
                LogFilters.State, gameObject);

            Query queryPath = FirebaseSDK.Instance.firebaseDb
                .Child(_roomKey)
                .Child(roomId)
                .Child(playerId);

            await FirebaseSDK.Instance.Async_SetData(queryPath, ((int)value),
                tId =>
                {
                    DebugX.Log($"{LogClassName} : Room State Set Successfully. Id:{tId}", LogFilters.State, gameObject);
                    return Task.CompletedTask;
                },
                (tId, err) =>
                {
                    DebugX.LogError($"{LogClassName} : Unable to Set Room State. Id:{tId} Reason: {err}",
                        LogFilters.State,
                        gameObject);
                    return Task.CompletedTask;
                });
        }

        public async Task<RoomKeyPlayerStatus> GetRoomPlayerStatus(string playerId, string roomId,
            RoomKeyPlayerStatus defaultValue)
        {
            DebugX.Log($"{LogClassName} : Fetch Room State Key: {roomId} for Player Id: {playerId}..", LogFilters.State,
                gameObject);

            Query query = FirebaseSDK.Instance.firebaseDb
                .Child(_roomKey)
                .Child(roomId)
                .Child(playerId);

            var tcs = new TaskCompletionSource<object>();

            await FirebaseSDK.Instance.Async_FetchData(query,
                snapshot =>
                {
                    DebugX.Log($"{LogClassName} : Room State Fetched Successfully.", LogFilters.State, gameObject);
                    tcs.SetResult(snapshot.Value);
                    return Task.CompletedTask;
                },
                err =>
                {
                    DebugX.Log($"{LogClassName} : Unable to Fetch Room State. Reason: {err}", LogFilters.State,
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

            if (tcs.Task.Result == null)
            {
                return defaultValue;
            }

            int value = Int32.Parse(tcs.Task.Result.ToString());
            return ((RoomKeyPlayerStatus)value);
        }

        public async void SetRoomStatus(string roomId, RoomKeyStatus status)
        {
            DebugX.Log($"{LogClassName} : Set Room State Key:{_roomStateKey} Value: {status.ToString()}..",
                LogFilters.State, gameObject);

            Query queryPath = FirebaseSDK.Instance.firebaseDb
                .Child(_roomKey)
                .Child(roomId)
                .Child(_roomStateKey);

            await FirebaseSDK.Instance.Async_SetData(queryPath, (int)status,
                tId =>
                {
                    DebugX.Log($"{LogClassName} : Room State Set Successfully. Id:{tId}", LogFilters.State, gameObject);
                    return Task.CompletedTask;
                },
                (tId, err) =>
                {
                    DebugX.Log($"{LogClassName} : Unable to Set Room State. Id:{tId} Reason: {err}", LogFilters.State,
                        gameObject);
                    return Task.CompletedTask;
                });
        }

        public async Task<RoomKeyStatus> GetRoomStatus(string roomId)
        {
            DebugX.Log($"{LogClassName} : Fetch Room State Key: {roomId}..", LogFilters.State, gameObject);

            Query query = FirebaseSDK.Instance.firebaseDb
                .Child(_roomKey)
                .Child(roomId)
                .Child(_roomStateKey);

            var tcs = new TaskCompletionSource<RoomKeyStatus>();

            await FirebaseSDK.Instance.Async_FetchData(query,
                snapshot =>
                {
                    DebugX.Log($"{LogClassName} : Room State Fetched Successfully.", LogFilters.State, gameObject);
                    RoomKeyStatus value = Enum.Parse<RoomKeyStatus>(snapshot.Value.ToString());
                    tcs.SetResult(value);
                    return Task.CompletedTask;
                },
                err =>
                {
                    DebugX.Log($"{LogClassName} : Unable to Fetch Room State. Reason: {err}", LogFilters.State,
                        gameObject);
                    tcs.SetResult(RoomKeyStatus.Closed);
                    return Task.CompletedTask;
                },
                tId =>
                {
                    tcs.SetResult(RoomKeyStatus.Closed);
                    return Task.CompletedTask;
                });

            await tcs.Task;

            return tcs.Task.Result;
        }

        public async Task<bool> DoesRoomEntryExist(string roomId, bool defaultValue)
        {
            DebugX.Log($"{LogClassName} : Fetch Room Entry Key: {roomId}..", LogFilters.State, gameObject);

            Query query = FirebaseSDK.Instance.firebaseDb
                .Child(_roomKey)
                .Child(roomId);

            var tcs = new TaskCompletionSource<object>();

            await FirebaseSDK.Instance.Async_FetchData(query,
                snapshot =>
                {
                    DebugX.Log($"{LogClassName} : Room Entry Fetched Successfully.", LogFilters.State, gameObject);
                    tcs.SetResult(true);
                    return Task.CompletedTask;
                },
                err =>
                {
                    DebugX.Log($"{LogClassName} : Unable to Fetch Room Entry. Reason: {err}", LogFilters.State,
                        gameObject);
                    tcs.SetResult(defaultValue);
                    return Task.CompletedTask;
                },
                tId =>
                {
                    tcs.SetResult(false);
                    return Task.CompletedTask;
                });

            await tcs.Task;

            return (bool)tcs.Task.Result;
        }

        public async void CleanUpRoomEntry(string roomId)
        {
            DebugX.Log($"{LogClassName} : Clean Room State Key for Id: {roomId} ..", LogFilters.State, gameObject);

            Query queryPath = FirebaseSDK.Instance.firebaseDb
                .Child(_roomKey)
                .Child(roomId);

            await FirebaseSDK.Instance.Async_DeleteData(queryPath,
                tId =>
                {
                    DebugX.Log($"{LogClassName} : Room State Clean Successfully. Id:{tId}", LogFilters.State,
                        gameObject);
                    return Task.CompletedTask;
                },
                (tId, err) =>
                {
                    DebugX.Log($"{LogClassName} : Unable to Clean Room State. Id:{tId} Reason: {err}", LogFilters.State,
                        gameObject);
                    return Task.CompletedTask;
                });
        }
    }
}