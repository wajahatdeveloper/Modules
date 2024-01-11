using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace NetworkLayer
{
    public class EnsureLobbyIsJoined : SingletonBehaviour<EnsureLobbyIsJoined>
    {
        private const string LogClassName = "EnsureLobbyIsJoined";

        private TaskCompletionSource<object> tcs;

        private void OnEnable()
        {
            PUNConnectionHandler.OnConnectionSuccess += Handle_OnConnectionSuccess;
            PUNConnectionHandler.OnConnectionFailure += Handle_OnConnectionFailure;
        }

        private void OnDisable()
        {
            PUNConnectionHandler.OnConnectionSuccess -= Handle_OnConnectionSuccess;
            PUNConnectionHandler.OnConnectionFailure -= Handle_OnConnectionFailure;
        }

        public async Task<object> Check()
        {
            if (tcs != null)
            {
                DebugX.Log($"{LogClassName} : Already Checking Lobby Connection..",LogFilters.None, gameObject);
                return Task.CompletedTask;
            }

            tcs = new TaskCompletionSource<object>();

            CheckServerConnection();

            await tcs.Task;

            return Task.CompletedTask;
        }

        private void CheckServerConnection()
        {
            if (!PhotonNetwork.IsConnected)
            {
                WaitPanel.Instance.Show("Connecting..");
                PUNConnectionHandler.ConnectToPUN();
            }
            else
            {
                CheckLobbyConnection();
            }
        }

        private void Handle_OnConnectionSuccess()
        {
            if (tcs == null) { return; }
            CheckLobbyConnection();
        }

        private void CheckLobbyConnection()
        {
            if (!PhotonNetwork.InLobby)
            {
                WaitPanel.Instance.Show("Joining Lobby ..");
                PUNLobbyHandler.OnJoinedLobbySuccess += new OnceAction(Handle_OnJoinedLobbySuccess).Invoke;
                PUNLobbyHandler.ConnectToLobby();
            }
            else
            {
                DebugX.Log($"{LogClassName} : Already In Lobby.",LogFilters.None, gameObject);
                tcs.SetResult(null);
            }
        }

        private void Handle_OnJoinedLobbySuccess()
        {
            DebugX.Log($"{LogClassName} : Lobby Join Success.",LogFilters.None, gameObject);
            WaitPanel.Instance.Hide();
            tcs.TrySetResult(null);
        }

        private void Handle_OnConnectionFailure(bool isExpected, DisconnectCause cause)
        {
            if (isExpected) { return; }

            PopupMessage.Instance.onClose.AddListener(RetryConnection);
            PopupMessage.Instance.Show($"Network Connection Failure.\nCheck Internet and Try Again.",
                "Connection Error");
        }

        private void RetryConnection()
        {
            PopupMessage.Instance.onClose.RemoveListener(RetryConnection);
            SceneManagerX.RestartCurrentScene();
        }
    }
}