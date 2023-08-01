using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;

public class PUNHostMigration : MonoBehaviourPunCallbacks
{
    [InfoBox("This Script Exposes the Following Events\n" +
             "OnHostMigrated<Player>")]
    public static Action<Player> OnHostMigrated;
    
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        OnHostMigrated?.Invoke(newMasterClient);
    }
}