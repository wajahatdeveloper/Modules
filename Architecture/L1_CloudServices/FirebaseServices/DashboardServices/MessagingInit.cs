using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Messaging;
using UnityEngine;

public class MessagingInit : MonoBehaviour
{
    private const string LogClassName = "FirebaseMessaging";

    private void OnEnable()
    {
        FirebaseSDK.OnFirebaseInit += Handle_OnFirebaseInit;
        FirebaseSDK.OnFirebaseInitFailed += Handle_OnFirebaseInitFailed;
    }
    
    private void Handle_OnFirebaseInit()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += Handle_TokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += Handle_MessageReceived;

        // Set a flag here for indicating that your project is ready to use Firebase.
        DebugX.Log($"{LogClassName} : Messaging Initialized Successfully.",LogFilters.None, null);
    }
    
    private void Handle_OnFirebaseInitFailed(string err)
    {
        DebugX.Log($"{LogClassName} : Messaging Not Initialized.",LogFilters.None, null);
    }

    private void Handle_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log($"Firebase Messaging : Message Received => {e.Message}");
    }

    private void Handle_TokenReceived(object sender, TokenReceivedEventArgs e)
    {
        Debug.Log($"Firebase Messaging : Token Received => {e.Token}");
    }
}