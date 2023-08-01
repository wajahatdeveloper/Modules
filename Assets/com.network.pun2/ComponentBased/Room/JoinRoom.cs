using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

public class JoinRoom : MonoBehaviour, IMatchmakingCallbacks , ILobbyCallbacks
{
    [Header("Room Name")]
    public string roomName;
    [Header("Join Random")]
    public bool joinRandomRoom = false;
    public byte expectedMaxPlayers;
    public MatchmakingMode matchmakingMode;
    public KeyValueDictionary expectedRoomProperties;
    public string sqlLobbyFilter;
    [Header("Typed Lobby")]
    public string lobby;
    public LobbyType lobbyType;
    [Header("Expected Players")]
    public string[] expectedUsers;

    [Header("Callbacks")]
    public UnityEvent onJoined;
    public UnityEvent<short,string> onJoinFailed;
    public UnityEvent<short,string> onJoinRandomFailed;

    private TypedLobby _typedLobby;
    private bool _isJoining = false;
    
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void Join()
    {
        _isJoining = true;
        
        _typedLobby = (lobby.IsNullOrEmpty())?null:new TypedLobby(lobby,lobbyType);

        if (PhotonNetwork.CurrentLobby.Name != lobby)
        {
            PhotonNetwork.JoinLobby(_typedLobby);
        }
        else
        {
            PhotonNetwork.JoinRoom(roomName,expectedUsers);
        }
    }

    private Hashtable GetCustomProperties()
    {
        Hashtable hashtable = new Hashtable();
        
        foreach (var property in expectedRoomProperties)
        {
            hashtable.Add(property.key,property.value);
        }

        return hashtable;
    }
    
    #region Callbacks

    public void OnFriendListUpdate(List<FriendInfo> friendList) { }

    public void OnCreatedRoom() { }

    public void OnCreateRoomFailed(short returnCode, string message) { }

    public void OnJoinedRoom()
    {
        _isJoining = false;
        DebugX.Log($"Room {roomName} Joined","PUN",gameObject);
        onJoined.InvokeIfNotNull();
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        DebugX.Log($"Room {roomName} Failed to Join","PUN",gameObject);
        onJoinFailed.InvokeIfNotNull(returnCode,message);
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        DebugX.Log($"Room {roomName} Failed to Join Random","PUN",gameObject);
        onJoinRandomFailed.InvokeIfNotNull(returnCode,message);
    }

    public void OnLeftRoom() { }

    public void OnJoinedLobby()
    {
        if (!_isJoining) { return; }
        DebugX.Log($"Switched Lobby to {lobby} for Joining Room {roomName}","PUN",gameObject);
        if (joinRandomRoom)
        {
            PhotonNetwork.JoinRandomRoom(GetCustomProperties(), expectedMaxPlayers, matchmakingMode, _typedLobby,
                sqlLobbyFilter, expectedUsers);
        }
        else
        {
            PhotonNetwork.JoinRoom(roomName,expectedUsers);
        }
    }

    public void OnLeftLobby() { }

    public void OnRoomListUpdate(List<RoomInfo> roomList) { }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) { }
    
    #endregion
}
