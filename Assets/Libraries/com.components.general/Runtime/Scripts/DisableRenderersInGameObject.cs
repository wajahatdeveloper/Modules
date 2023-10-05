using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableRenderersInGameObject : MonoBehaviour
{
    public bool ExecuteOnEnable = false;
    
    public void OnEnable()
    {
        if (ExecuteOnEnable)
        {
            Execute(gameObject);
        }
    }

    public void Execute(GameObject obj)
    {
        foreach (MeshRenderer meshRenderer in obj.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.enabled = false;
        }
    }
}
