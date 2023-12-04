using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

[InfoBox("This Script Exposes the Following Events\n" +
         "OnJoinedLobbySuccess\n" +
         "OnLeftLobbySuccess\n" +
         "OnRoomListUpdated<List<RoomInfo>>")]
public class PUNLobbyHandler : MonoBehaviourPunCallbacks
{
    private const string LogClassName = "PUNLobbyHandler";

    public static event Action OnJoinedLobbySuccess;
    public static event Action OnLeftLobbySuccess;
    public static event Action<List<RoomInfo>> OnRoomListUpdated;

    private static Dictionary<string, RoomInfo> _CachedRoomList = new();
    
    public static void ConnectToLobby()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            var lobby = TypedLobby.Default;
            if (PhotonNetwork.OfflineMode)
            {
                OnJoinedLobbyOffline();
            }
            else
            {
                PhotonNetwork.JoinLobby(lobby);
            }
            DebugX.Log($"{LogClassName} : Connecting to Lobby {lobby}..",LogFilters.Network, null);
        }
        else
        {
            DebugX.Log($"{LogClassName} : PUN not Connected.", Color.red, LogFilters.Network, null);
        }
    }

    private static void OnJoinedLobbyOffline()
    {
        DebugX.Log($"{LogClassName} : Joined Lobby.", LogFilters.Network, null);
        OnJoinedLobbySuccess?.Invoke();
        _CachedRoomList.Clear();
    }

    public override void OnJoinedLobby()
    {
        DebugX.Log($"{LogClassName} : Joined Lobby.",LogFilters.Network, gameObject);
        OnJoinedLobbySuccess?.Invoke();

        _CachedRoomList.Clear();
    }
    
    public override void OnLeftLobby()
    {
        DebugX.Log($"{LogClassName} : On Left Lobby.",LogFilters.Network, gameObject);
        OnLeftLobbySuccess?.Invoke();
        
        _CachedRoomList.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            if (info.RemovedFromList)
            {
                _CachedRoomList.Remove(info.Name);
            }
            else
            {
                _CachedRoomList[info.Name] = info;
            }
        }
        
        OnRoomListUpdated?.Invoke(roomList);
        
        DebugX.Log($"{LogClassName} : Lobby Room List Updated.\n" +
                   $"{_CachedRoomList.ToStringFull()}",
            LogFilters.Network, gameObject);
    }

    public static bool RoomExists_Cached(string roomId)
    {
        return _CachedRoomList.ContainsKey(roomId);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _CachedRoomList.Clear();
    }

    public static Dictionary<string, RoomInfo> GetCachedRoomList()
    {
        return _CachedRoomList;
    }
}