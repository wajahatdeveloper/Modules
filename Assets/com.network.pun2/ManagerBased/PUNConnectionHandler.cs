using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;

public class PUNConnectionHandler : MonoBehaviourPunCallbacks
{
    public bool autoConnectToPunOnStart = true;
    public bool isOfflineMode = false;
    
    [InfoBox("This Script Exposes the Following Events\n" +
             "OnConnectionSuccess\n"+
             "OnConnectionFailure<DisconnectCause>")]
    public static Action OnConnectionSuccess;
    public static Action<DisconnectCause> OnConnectionFailure;

    private static string LogClassName = "PUNConnectionHandler"; 
    
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.OfflineMode = isOfflineMode;

        if (autoConnectToPunOnStart)
        {
            ConnectToPUN();
        }
    }

    public static void ConnectToPUN()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            DebugX.Log($"{LogClassName} : Already Connected to PUN.",LogFilters.Network, null);
            return;
        }

        PhotonNetwork.ConnectUsingSettings();
        DebugX.Log($"{LogClassName} : Connecting to PUN..",LogFilters.Network, null);
    }

    public static void DisconnectFromPUN()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            DebugX.Log($"{LogClassName} : Already Disconnected from PUN.",LogFilters.Network, null);
            return;
        }
        
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        DebugX.Log($"{LogClassName} : OnConnectedToMaster.",LogFilters.Network,gameObject);
        OnConnectionSuccess?.Invoke();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        DebugX.Log($"{LogClassName} : OnDisconnected({cause})",LogFilters.Network,gameObject);
        OnConnectionFailure?.Invoke(cause);
    }
}