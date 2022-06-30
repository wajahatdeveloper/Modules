using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonNetworkX : ILobbyCallbacks
{
    public static List<RoomInfo> RoomInfos = new List<RoomInfo>();
    
    public static List<RoomInfo> GetRoomList() { return PhotonNetwork.InLobby ? RoomInfos : null; }
    
    public void OnJoinedLobby() { }
    
    public void OnLeftLobby() { }
    
    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) { }
    
    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RoomInfos = new List<RoomInfo>();
        RoomInfos.AddRange(roomList);
    }
}
