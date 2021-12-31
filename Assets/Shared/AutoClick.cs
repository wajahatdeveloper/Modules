using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoClick : MonoBehaviour
{
    public float delay = 0.2f;
    
    private void OnEnable()
    {
        Invoke(nameof(Execute),delay);
    }

    public void Execute()
    {
        GetComponent<Button>().onClick?.Invoke();
    }
}
