using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

[InfoBox("This Script Exposes the Following Events\n" +
         "OnCreateRoomSuccess\n" +
         "OnCreateRoomFailed_<string>\n" +
         "OnJoinRoomSuccess\n" +
         "OnJoinRoomFailed_<string>\n" +
         "OnLeaveRoomSuccess\n" +
         "OnRemotePlayerEnteredRoom<Player>\n" +
         "OnRemotePlayerLeftRoom<Player>\n" +
         "OnRoomPropertiesUpdated<Hashtable>\n" +
         "OnPlayerPropertiesUpdated<Player, Hashtable>\n")]
public class PUNRoomHandler : MonoBehaviourPunCallbacks
{
    public static bool IsLeavingRoomExplicitly = false;

    private const string LogClassName = "PUNRoomHandler";

    public static event Action OnCreateRoomSuccess;
    public static event Action<string> OnCreateRoomFailed_;
    
    public static event Action OnJoinRoomSuccess;
    public static event Action<string> OnJoinRoomFailed_;

    public static event Action OnLeaveRoomSuccess;

    public static event Action<Player> OnRemotePlayerEnteredRoom;
    public static event Action<Player> OnRemotePlayerLeftRoom;

    public static event Action<Hashtable> OnRoomPropertiesUpdated;
    public static event Action<Player, Hashtable> OnPlayerPropertiesUpdated;

    #region ProcessFunctions

    public static void JoinOrCreateRoom(
        string 	    roomName,
        RoomOptions roomOptions = null,
        TypedLobby 	typedLobby = null,
        string[] 	expectedUsers = null 
    )
    {
        DebugX.Log($"{LogClassName} : Joining / Creating Room by Id : {roomName}..",LogFilters.Network, null);

        if (!PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, expectedUsers))
        {
            DebugX.Log($"{LogClassName} : Join / Create Room Failed (False Return).", Color.red, LogFilters.Network, null);
            OnCreateRoomFailed_?.Invoke("Join / Create Room Failed (False Return)");
        }
    }
    
    public static void JoinRandomOrCreateRoom(
        Hashtable expectedCustomRoomProperties = null,
        byte expectedMaxPlayers = 0,
        MatchmakingMode matchmakingMode = MatchmakingMode.FillRoom,
        string sqlLobbyFilter = null,
        string 	    roomName = null,
        RoomOptions roomOptions = null,
        TypedLobby 	typedLobby = null,
        string[] 	expectedUsers = null
    )
    {
        DebugX.Log($"{LogClassName} : Joining Random or Creating Room by Id if not available : {roomName}..",LogFilters.Network, null);

        if (!PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties, expectedMaxPlayers,
                matchmakingMode, typedLobby, sqlLobbyFilter, roomName, roomOptions, expectedUsers))
        {
            DebugX.Log($"{LogClassName} : Join Random / Create Room Failed (False Return).", Color.red, LogFilters.Network, null);
            OnCreateRoomFailed_?.Invoke("Join Random / Create Room Failed (False Return)");
        }
    }

    #endregion
    
    #region Room Creation

    public static void CreateRoom(
        string 	    roomName,
        RoomOptions roomOptions = null,
        TypedLobby 	typedLobby = null,
        string[] 	expectedUsers = null 
    )
    {
        DebugX.Log($"{LogClassName} : Creating Room by Id : {roomName}..",LogFilters.Network, null);

        if (!PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, expectedUsers))
        {
            DebugX.Log($"{LogClassName} : Create Room Failed (False Return).", Color.red, LogFilters.Network, null);
            OnCreateRoomFailed_?.Invoke("Create Room Failed (False Return)");
        }
    }

    public override void OnCreatedRoom()
    {
        DebugX.Log($"{LogClassName} : Created Room Successfully.",LogFilters.Network, null);
        OnCreateRoomSuccess?.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        OnCreateRoomFailed_?.Invoke($"Create Room Failed with Code: {returnCode} message : {message}");
    }

    #endregion

    #region Room Joining

    public static void RejoinRoom(string roomName)
    {
        DebugX.Log($"{LogClassName} : Rejoining Room by Id : {roomName}..",LogFilters.Network, null);

        if (!PhotonNetwork.RejoinRoom(roomName))
        {
            DebugX.Log($"{LogClassName} : Rejoin Room Failed (False Return).", Color.red, LogFilters.Network, null);
        }
    }
    
    public static void JoinRoom(
        string 	  roomName,
        string[]  expectedUsers = null
        )
    {
        DebugX.Log($"{LogClassName} : Joining Room by Id : {roomName}..",LogFilters.Network, null);

        if (!PhotonNetwork.JoinRoom(roomName, expectedUsers))
        {
            DebugX.Log($"{LogClassName} : Join Room Failed (False Return).", Color.red, LogFilters.Network, null);
        }
    }

    public override void OnJoinedRoom()
    {
        IsLeavingRoomExplicitly = false;
        
        DebugX.Log($"{LogClassName} : Joined Room Successfully.",LogFilters.Network, null);

        OnJoinRoomSuccess?.Invoke();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        OnJoinRoomFailed_?.Invoke($"Join Room Failed with Code: {returnCode} message : {message}");
    }

    #endregion

    #region Room Leaving

    public static void LeaveRoom(bool becomeInactive = true)
    {
        if (PhotonNetwork.InRoom)
        {
            IsLeavingRoomExplicitly = true;
            DebugX.Log($"{LogClassName} : Leaving Room..",LogFilters.Network, null);
            PhotonNetwork.LeaveRoom(becomeInactive);
        }
        else
        {
            DebugX.Log($"{LogClassName} : Leave Room Failed (Is not in Room).",LogFilters.Network, null);
        }
    }

    public override void OnLeftRoom()
    {
        DebugX.Log($"{LogClassName} : Left Room Successfully.",LogFilters.Network, null);
        OnLeaveRoomSuccess?.Invoke();
    }

    #endregion

    #region RemotePlayer

    // On Remote Player Entered Room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        DebugX.Log($"{LogClassName} : Remote Player {newPlayer.NickName} Entered the Room", LogFilters.Network,gameObject);

        OnRemotePlayerEnteredRoom?.Invoke(newPlayer);
    }
    
    // On Remote Player Left Room
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        string info = otherPlayer.IsInactive ? "Inactive" : "Explicit";
        DebugX.Log($"{LogClassName} : Remote Player {otherPlayer.NickName} Left the Room. ({info})", LogFilters.Network,gameObject);

        OnRemotePlayerLeftRoom?.Invoke(otherPlayer);
    }

    #endregion

    #region RoomProperties

    public static void SetRoomProperties(ExitGames.Client.Photon.Hashtable hashtable)
    {
        DebugX.Log($"Room Props = {hashtable.ToStringFull()}",LogFilters.Network,null);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }
    
    public static ExitGames.Client.Photon.Hashtable GetRoomProperties()
    {
        return PhotonNetwork.CurrentRoom.CustomProperties;
    }
    
    public static object GetRoomProperty(string key, object defaultValue)
    {
        return PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(key, out var value) ? value : defaultValue;
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        DebugX.Log($"{LogClassName} : {propertiesThatChanged.ToStringFull()}", LogFilters.Network,gameObject);

        OnRoomPropertiesUpdated?.Invoke(propertiesThatChanged);
    }

    #endregion

    #region PlayerProperties

    public static void SetLocalPlayerProperties(Hashtable hashtable)
    {
        DebugX.Log($"Local Player Props => {hashtable.ToStringFull()}",LogFilters.Network,null);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
    }
    
    public static Hashtable GetLocalPlayerProperties()
    {
        return PhotonNetwork.LocalPlayer.CustomProperties;
    }
    
    public static object GetLocalPlayerProperty(string key, object defaultValue)
    {
        return PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(key, out var value) ? value : defaultValue;
    }

    public static object GetPlayerProperty(Player player, string key, object defaultValue)
    {
        return player.CustomProperties.TryGetValue(key, out var value) ? value : defaultValue;
    }
    
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        DebugX.Log($"{LogClassName} : {targetPlayer.NickName} => {changedProps.ToStringFull()}", LogFilters.Network,gameObject);

        OnPlayerPropertiesUpdated?.Invoke(targetPlayer, changedProps);
    }

    #endregion

    public static void SetPlayerProperties(Player player, Hashtable hashtable)
    {
        DebugX.Log($"Player {player.NickName} Props => {hashtable.ToStringFull()}",LogFilters.Network,null);
        player.SetCustomProperties(hashtable);
    }
}