using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using UnityEngine;

namespace GameFeatures
{
    public class AutoRejoinRoom : SingletonBehaviour<AutoRejoinRoom>
    {
        private const string LogClassName = "AutoRejoinRoom";

         private void OnUnableToReJoin()
        {
            DebugX.Log($"{LogClassName} : Unable to Rejoin : Restarting Scene.",LogFilters.None, null);
            LocalData.Instance.IsPlayerRejoining = false;
            GlobalEvents.OnDisableRejoin.InvokeSafe();
            SceneManagerX.RestartCurrentScene();
        }

        public void AttemptRejoin(bool isManual = false)
        {
            if (!LocalData.Instance.GetIsRejoinAvailable_Persistent()) { return; }

            string rejoinRoomId = LocalData.Instance.GetRejoinRoomId_Persistent();

            switch (DefaultData.Instance.gameSettings.rejoinSetting)
            {
                case RejoinSetting.NoRejoin:
                    DebugX.Log($"{LogClassName} : Rejoining is Disabled.",LogFilters.None, null);
                    return;

                case RejoinSetting.AutoRejoin:
                    DebugX.Log($"{LogClassName} : Auto Rejoin Room with ID : {rejoinRoomId}.",LogFilters.None, null);
                    break;

                case RejoinSetting.ManualRejoin:
                    if (!isManual) { return; }
                    DebugX.Log($"{LogClassName} : Manual Rejoin Room with ID : {rejoinRoomId}.",LogFilters.None, null);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            RejoinRoom(OnUnableToReJoin);
        }

        private async void RejoinRoom(Action onUnableToRejoin = null)
        {
            string rejoinRoomId = LocalData.Instance.GetRejoinRoomId_Persistent();

            LoadingPanel.Instance.Show(); // loading persists thorough scene loading

            await Task.Delay(3500);     // Grace period to allow room list to update

            if (PUNLobbyHandler.RoomExists_Cached(rejoinRoomId))
            {
                await AttemptRejoinExistingRoom(rejoinRoomId, onUnableToRejoin);
            }
            else
            {
                onUnableToRejoin?.Invoke();
            }
        }

        private async Task AttemptRejoinExistingRoom(string roomId, Action onUnableToRejoin = null)
        {
            DebugX.Log($"{LogClassName} : {nameof(AttemptRejoinExistingRoom)}",LogFilters.None, null);

            var roomStatus = await RoomStateDataHandler.Instance.GetRoomStatus(roomId);

            bool isRoomValidForRejoining = false;

            switch (roomStatus)
            {
                case RoomStateDataHandler.RoomKeyStatus.InLobby:
                case RoomStateDataHandler.RoomKeyStatus.InSession:
                    isRoomValidForRejoining = true;
                    break;

                case RoomStateDataHandler.RoomKeyStatus.InConclusion:
                case RoomStateDataHandler.RoomKeyStatus.Closed:
                    isRoomValidForRejoining = false;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (isRoomValidForRejoining)
            {
                string userId = LocalData.Instance.GetPlayerUserId_Persistent();

                // Get the player status
                var status = await RoomStateDataHandler.Instance.GetRoomPlayerStatus(userId,
                    roomId, RoomStateDataHandler.RoomKeyPlayerStatus.UnAvailable);

                switch (status)
                {
                    case RoomStateDataHandler.RoomKeyPlayerStatus.UnAvailable:
                    case RoomStateDataHandler.RoomKeyPlayerStatus.Left:
                        onUnableToRejoin?.Invoke();
                        break;

                    case RoomStateDataHandler.RoomKeyPlayerStatus.Disconnected:
                    case RoomStateDataHandler.RoomKeyPlayerStatus.Present:
                        LocalData.Instance.IsPlayerRejoining = true;
                        PUNRoomHandler.RejoinRoom(roomId); // Rejoin previous room
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                onUnableToRejoin?.Invoke();
            }
        }
    }
}