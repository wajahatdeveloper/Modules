using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CheckOtherPlayerConnection : MonoBehaviourPunCallbacks
{
    public bool leaveRoomOnDisconnect = false;
    public UnityEvent onLeftRoom;
    [Space]
    public UnityEvent<Player> onOtherPlayerDisconnect;

    public override void OnEnable()
    {
        DebugX.Log("Monitoring Other Player Connection...","PUN",gameObject);

        PhotonNetwork.AddCallbackTarget(this);
    }
    
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        string message = $"Other Player left the room";
        DebugX.Log(message,"PUN",gameObject);
        PopupMessage.Instance.Show(message);
        PopupMessage.Instance.onClose.Once(() =>
        {
            onOtherPlayerDisconnect.InvokeIfNotNull(otherPlayer);
            
            if (leaveRoomOnDisconnect) { PhotonNetwork.LeaveRoom(); }
        });
    }

    public override void OnLeftRoom()
    {
        DebugX.Log("Player Left Room","PUN",gameObject);
       onLeftRoom.InvokeIfNotNull();
    }
}
