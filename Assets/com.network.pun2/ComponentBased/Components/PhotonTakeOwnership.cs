using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonTakeOwnership : MonoBehaviour
{
    public PhotonView photonView;
    
    public bool onEnable = true;

    private void OnEnable()
    {
        if (onEnable)
        {
            photonView.RequestOwnership();
        }
    }

    public void TakeOwnership()
    {
        photonView.RequestOwnership();
    }
}
