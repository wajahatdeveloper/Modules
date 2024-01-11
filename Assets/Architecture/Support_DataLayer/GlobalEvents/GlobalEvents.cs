using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public static class GlobalEvents
{
    /* MAIN MENU */
    public static Action<string> OnEnableRejoin;    // Set Room Rejoin Data <room id>
    public static Action OnDisableRejoin;           // Clear Room Rejoin Data

    /* GAMEPLAY */
    public static Action OnLocalPlayerLost;         // Notify That Local Player Lost the Match
}