using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;

public class PUNLobbyHandler : MonoBehaviourPunCallbacks
{
    private static string LogClassName = "PUNLobbyHandler"; 
    
    [InfoBox("This Script Exposes the Following Events\n" +
             "OnJoinedLobbySuccess\n"+
             "OnLeftLobbySuccess\n"+
             "OnRoomListUpdated<List<RoomInfo>>")]
    public static Action OnJoinedLobbySuccess;
    public static Action OnLeftLobbySuccess;
    public static Action<List<RoomInfo>> OnRoomListUpdated;
    
    public static void ConnectToLobby()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            var lobby = TypedLobby.Default;
            PhotonNetwork.JoinLobby(lobby);
            DebugX.Log($"{LogClassName} : Connecting to Lobby {lobby}..",LogFilters.Network, null);
        }
        else
        {
            DebugX.Log($"{LogClassName} : PUN not Connected.", Color.red, LogFilters.Network, null);
        }
    }

    public override void OnJoinedLobby()
    {
        DebugX.Log($"{LogClassName} : Joined Lobby.",LogFilters.Network, gameObject);
        OnJoinedLobbySuccess?.Invoke();
    }
    
    public override void OnLeftLobby()
    {
        DebugX.Log($"{LogClassName} : On Left Lobby.",LogFilters.Network, gameObject);
        OnLeftLobbySuccess?.Invoke();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        DebugX.Log($"{LogClassName} : Lobby Room List Updated.",LogFilters.Network, gameObject);
        OnRoomListUpdated?.Invoke(roomList);
    }
}