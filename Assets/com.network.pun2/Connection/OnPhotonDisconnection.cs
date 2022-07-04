using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class OnPhotonDisconnection : MonoBehaviour
{
    public UnityEvent onDisconnected;

    private void OnEnable()
    {
        if (PhotonNetwork.IsConnected) return;
        
        string message = "Unable to Connect to Photon Master Server, Refresh the Page and Try Again";
        DebugX.Log(message,"PUN",gameObject);
        PopupMessage.Instance.ShowAuto(message);
        onDisconnected.InvokeIfNotNull();
    }
}
