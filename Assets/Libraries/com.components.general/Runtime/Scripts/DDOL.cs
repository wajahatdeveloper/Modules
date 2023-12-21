using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DDOL : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Debug.Log("The GameObject \"" + gameObject.name + "\" was set as DontDestroyOnLoad", this);
    }
}