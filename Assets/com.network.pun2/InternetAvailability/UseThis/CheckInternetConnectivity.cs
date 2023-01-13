using System;
using System.Runtime.InteropServices;
using GleyInternetAvailability;
using InlineCoroutine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckInternetConnectivity : MonoBehaviour
{
    public bool checkOnEnable = true;
    public bool showReconnectionDialog = true;
    [Header("No checking if interval is 0")]
    public float checkInterval = 2.0f;
    [Space]
    public UnityEvent onInternetDisconnected;
    
    [DllImport("__Internal")]
    private static extern void reloadPage();
    
    private void OnEnable()
    {
        if (checkOnEnable)
        {
            CheckConnection();
        }

        if (checkInterval > 0)
        {
            InvokeRepeating(nameof(CheckConnection),0.1f,checkInterval);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void CheckConnection()
    {
        GleyInternetAvailability.Network.IsAvailable(CompleteMethod);
    }

    //this method will be automatically called when check is complete
    private void CompleteMethod(ConnectionResult connectionResult)
    {
        //if connection result is "Working" user has Internet access not just his card enabled
        if(connectionResult == ConnectionResult.Working)
        {
            /// "Network connection is available";
        }
        else
        {
            Debug.LogError("Network connection is unavailable");
            CancelInvoke();
            onInternetDisconnected?.Invoke();
            if (showReconnectionDialog)
            {
                Dialog.Instance.Show("Network connection is unavailable, \n This can be due to networking issues, make sure you are connected to the internet  \nClick yes to try again or no to refresh the page","Disconnection",
                    () =>
                    {
                        InvokeRepeating(nameof(CheckConnection),0.1f,checkInterval);
                    },
                    () =>
                    {
                        reloadPage();
                    });
            }
        }
    }
}
