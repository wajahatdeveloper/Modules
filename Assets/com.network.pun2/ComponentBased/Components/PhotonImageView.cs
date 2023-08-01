using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PhotonImageView : MonoBehaviourPun, IPunObservable
{
    public Image image;
    public List<Sprite> sprites = new List<Sprite>();

    private string spriteName = "";
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            spriteName = image.sprite.name;
            stream.SendNext(spriteName);
        }
        else
        {
            spriteName = stream.ReceiveNext().ToString();
            image.sprite = sprites.FirstOrDefault(x => x.name == spriteName);
        }
    }
}
