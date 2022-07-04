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

    public static IEnumerator WaitLock(string lockId, int lockCount,float checkInterval = 0.1f)
    {
        // Initialization
        string key = "waitLock_" + lockId;
        Debug.Log($"<color=magenta>Waiting for {key} 0/{lockCount}</color>");
        if (PhotonNetwork.IsMasterClient)
        {
            if (!PhotonNetwork.CurrentRoom.HasVariable(key))
            {
                Debug.Log($"<color=magenta>Remote {key} Created</color>");
                PhotonNetwork.CurrentRoom.SetVariable_Int(key,lockCount);
            }
            else
            {
                ReleaseLock(key,lockCount);
            }
        }
        else
        {
            while (!PhotonNetwork.CurrentRoom.HasVariable(key))
            {
                yield return new WaitForSecondsRealtime(1.0f);  // Wait for master to create the lock
            }
            
            ReleaseLock(key,lockCount);
        }

        yield return new WaitForSecondsRealtime(1.0f);  // To allow network value to sync

        int currentLockValue = PhotonNetwork.CurrentRoom.GetVariable_Int(key);
        int previousLockValue = currentLockValue;
        
        // Wait Loop
        while (currentLockValue > 0)       // Value will be changed over network
        {
            currentLockValue = PhotonNetwork.CurrentRoom.GetVariable_Int(key);
            if (currentLockValue != previousLockValue)
            {
                previousLockValue = currentLockValue;
                Debug.Log($"<color=magenta>{key} Lock Released {currentLockValue}/{lockCount}</color>");
            }
            yield return new WaitForSecondsRealtime(checkInterval);
        }
        
        // Resolution
        Debug.Log($"<color=magenta>{key} Lock Resolved</color>");
    }

    public static void ReleaseLock(string lockId, int lockCount)
    {
        string key = lockId;
        int currentLockValue = PhotonNetwork.CurrentRoom.GetVariable_Int(key);
        currentLockValue--;
        PhotonNetwork.CurrentRoom.SetVariable_Int(key,currentLockValue);
        Debug.Log($"<color=magenta>{key} Lock Released {currentLockValue}/{lockCount}</color>");
    }
}
