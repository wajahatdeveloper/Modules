using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class ConnectToPhoton : MonoBehaviourPunCallbacks
{
    public UnityEvent onConnectionSuccessful;
    public UnityEvent onConnectionFailed;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            DebugX.Log("Already Connected to Photon Master Server","PUN",gameObject);
            return;
        }
        
        DebugX.Log("Attempting to connect to Photon Master Server","PUN",gameObject);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        DebugX.Log("Connected to Photon Master Server","PUN",gameObject);
        onConnectionSuccessful.InvokeIfNotNull();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        string message = "Unable to Connect to Photon Master Server, Refresh the Page and Try Again";
        DebugX.Log(message,"PUN",gameObject);
        PopupMessage.Instance.ShowAuto(message);
        onConnectionFailed.InvokeIfNotNull();
    }
}
