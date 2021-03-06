using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Root<M,V,C> : RMVCCSingleton<Root<M,V,C>>
    where M : Model
    where V : View
    where C : Controller<M,V>
{
    public M Model;
    public C Controller;
    public V View;

    [TextArea] public string Description;

    private void OnEnable()
    {
        Debug.Log("Entered Gameplay Scene");
        
        InjectMockData();
    }

    private void OnDisable()
    {
        Debug.Log("Left Gameplay Scene");
    }

    protected virtual void InjectMockData()
    {
        Debug.Log("Mock Data Injected");
    }
}

#region Singleton
public class RMVCCSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
            }
            return instance;
        }
    }
}
#endregion