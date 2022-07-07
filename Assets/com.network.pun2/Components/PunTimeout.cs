using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PunTimeout : MonoBehaviour
{
    public float timer = 120;
    
    private void OnEnable()
    {
        if (!PhotonNetwork.IsConnected) { return; }
        StartCoroutine(Timeout());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Timeout()
    {
        while (timer > 0)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            timer--;
        }
        PopupMessage.Instance.onClose.Once(PhotonNetwork.Disconnect);
        PopupMessage.Instance.Show("Network Timeout Occured","Network Issue",PopupMessage.PopupSign.WARNING);
        WaitPanel.Instance.Hide();
    }
}
