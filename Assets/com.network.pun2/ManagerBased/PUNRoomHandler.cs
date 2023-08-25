using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PUNRoomHandler : MonoBehaviourPunCallbacks
{
    private static string LogClassName = "PUNRoomHandler";

    [InfoBox("This Script Exposes the Following Events\n" +
             "OnCreateRoomSuccess\n"+
             "OnJoinRoomSuccess\n"+
             "OnLeaveRoomSuccess\n"+
             "OnCreateRoomFailed_<string>\n"+
             "OnJoinRoomFailed_<string>\n"+
             "OnRemotePlayerEnteredRoom<Player>\n"+
             "OnRemotePlayerLeftRoom<Player>\n"+
             "OnRoomPropertiesUpdated<Hashtable>\n"+
             "OnPlayerPropertiesUpdated<Player,Hashtable>\n"
             )]
    public static Action OnCreateRoomSuccess;
    public static Action<string> OnCreateRoomFailed_;
    
    public static Action OnJoinRoomSuccess;
    public static Action<string> OnJoinRoomFailed_;

    public static Action OnLeaveRoomSuccess;

    public static Action<Player> OnRemotePlayerEnteredRoom;
    public static Action<Player> OnRemotePlayerLeftRoom;

    public static Action<Hashtable> OnRoomPropertiesUpdated;
    public static Action<Player, Hashtable> OnPlayerPropertiesUpdated;

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
        OnRemotePlayerEnteredRoom?.Invoke(newPlayer);
    }
    
    // On Remote Player Left Room
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        OnRemotePlayerLeftRoom?.Invoke(otherPlayer);
    }

    #endregion

    #region RoomProperties

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        OnRoomPropertiesUpdated?.Invoke(propertiesThatChanged);
    }

    #endregion

    #region PlayerProperties

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        OnPlayerPropertiesUpdated?.Invoke(targetPlayer, changedProps);
    }

    #endregion
}