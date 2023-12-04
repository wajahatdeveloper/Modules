using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;

[InfoBox("This Script Exposes the Following Events\n" +
         "OnConnectionSuccess\n" +
         "OnConnectionFailure<bool, DisconnectCause>")]
public class PUNConnectionHandler : MonoBehaviourPunCallbacks
{
    public static bool WasDisconnectionExpected { get; private set; }

    public bool autoConnectToPunOnStart = true;
    public bool isOfflineMode = false;
    
#if UNITY_EDITOR
    [Header("Editor Only")]
    public bool ignoreExpectedApplicationQuit = false;
#endif

    public static event Action OnConnectionSuccess;
    public static event Action<bool, DisconnectCause> OnConnectionFailure;

    private const string LogClassName = "PUNConnectionHandler";

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.OfflineMode = isOfflineMode;

        if (isOfflineMode)
        {
            string msg = "!!!!!!!!!!".Concat(" CAUTION: Offline Mode Is Enabled ").Bold().Concat("!!!!!!!!!!!!!!");
            Debug.Log(msg.Color(Color.magenta));
        }

        if (autoConnectToPunOnStart)
        {
            ConnectToPUN();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == true)
        {
            PhotonNetwork.KeepAliveInBackground = 1;
        }
    }

    public static void ConnectToPUN()
    {
        WasDisconnectionExpected = true;
        
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
        bool isExpected = false;
        
        switch (cause)
        {
            case DisconnectCause.ServerAddressInvalid:
            case DisconnectCause.DisconnectByServerLogic:
            case DisconnectCause.DisconnectByClientLogic:
            case DisconnectCause.DisconnectByDisconnectMessage:
            case DisconnectCause.ApplicationQuit:
                isExpected = true;
                break;
        }

#if UNITY_EDITOR
        if (cause == DisconnectCause.ApplicationQuit && ignoreExpectedApplicationQuit)
        {
            isExpected = false;
        }
#endif

        WasDisconnectionExpected = isExpected;
        
        DebugX.Log($"{LogClassName} : OnDisconnected({cause}) isExpected : {isExpected}",LogFilters.Network,gameObject);
        OnConnectionFailure?.Invoke(isExpected,cause);
    }
}