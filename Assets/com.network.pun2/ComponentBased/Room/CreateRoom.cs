using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

public class CreateRoom : MonoBehaviour, IMatchmakingCallbacks
{
    [Header("Room Name")]
    public string roomName;
    public bool generateRandomName = false;
    [Header("Join Or Create Room")]
    public bool useJoinOrCreateRoom = false;
    
    [Header("Room Options")]
    public byte maxPlayers;
    public int playerTtl;
    public int emptyRoomTtl;
    public bool isOpen;
    public bool isVisible;
    public bool cleanupCacheOnLeave;
    public bool suppressRoomEvents;
    public bool suppressPlayerInfo;
    public bool publishUserId;
    public bool deleteNullProperties;
    public bool broadcastPropsChangeToAll;
    [Space]
    public KeyValueDictionary customRoomProperties;
    [Space]
    public string[] customRoomPropertiesForLobby;
    [Space]
    public string[] plugins;
    [Header("Typed Lobby")]
    public string lobby;
    public LobbyType lobbyType;
    [Header("Expected Players")]
    public string[] expectedUsers;

    [Header("Callbacks")]
    public UnityEvent onCreated;
    public UnityEvent<short,string> onCreateFailed;
    public UnityEvent onJoined;
    public UnityEvent<short,string> onJoinFailed;

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void Create()
    {
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = maxPlayers,
            PlayerTtl = playerTtl,
            EmptyRoomTtl = emptyRoomTtl,
            IsOpen = isOpen,
            IsVisible = isVisible,
            CleanupCacheOnLeave = cleanupCacheOnLeave,
            SuppressRoomEvents = suppressRoomEvents,
            SuppressPlayerInfo = suppressPlayerInfo,
            PublishUserId = publishUserId,
            DeleteNullProperties = deleteNullProperties,
            BroadcastPropsChangeToAll = broadcastPropsChangeToAll,
            CustomRoomProperties = GetCustomProperties(),
            CustomRoomPropertiesForLobby = customRoomPropertiesForLobby,
            Plugins = plugins,
        };
        
        TypedLobby typedLobby = (lobby.IsNullOrEmpty())?null:new TypedLobby(lobby,lobbyType);
        
        if (generateRandomName) { roomName = Random.Range(1111, 9999).ToString("0000"); }

        if (useJoinOrCreateRoom)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
        }
        else
        {
            PhotonNetwork.CreateRoom(roomName,roomOptions,typedLobby,expectedUsers);
        }
    }

    private Hashtable GetCustomProperties()
    {
        Hashtable hashtable = new Hashtable();
        
        foreach (var property in customRoomProperties)
        {
            hashtable.Add(property.key,property.value);
        }

        return hashtable;
    }

    #region Callbacks

    public void OnFriendListUpdate(List<FriendInfo> friendList) { }

    public void OnCreatedRoom()
    {
        DebugX.Log($"Room {roomName} Created","PUN",gameObject);
        onCreated.InvokeIfNotNull();
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        DebugX.Log($"Room {roomName} Failed to Create","PUN",gameObject);
        onCreateFailed.InvokeIfNotNull(returnCode,message);
    }

    public void OnJoinedRoom()
    {
        DebugX.Log($"Room {roomName} Joined","PUN",gameObject);
        onJoined.InvokeIfNotNull();
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        DebugX.Log($"Room {roomName} Failed to Join","PUN",gameObject);
        onJoinFailed.InvokeIfNotNull(returnCode,message);
    }

    public void OnJoinRandomFailed(short returnCode, string message) { }

    public void OnLeftRoom() { }
    
    #endregion
}
