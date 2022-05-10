using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [AutoRef]
    public Transform t;

    private int i = 0;
    
    private void Update()
    {
        i++;
        GraphDbg.Log(i);
    }
}
