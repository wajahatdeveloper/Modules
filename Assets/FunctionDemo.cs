using UnityEngine;
using System.Collections;
using System.Reflection;
 
public class FunctionDemo : MonoBehaviour
{
    public string MethodToCall;
 
    void Start()
    {
        typeof(FunctionDemo)
            .GetMethod(MethodToCall, BindingFlags.Instance |BindingFlags.NonPublic | BindingFlags.Public)
            .Invoke(this, new object[0]);
    }
 
    void Update()
    {
 
    }
 
    void Foo()
    {
        Debug.Log("Foo");
    }
 
    void Bar()
    {
        Debug.Log("Bar");
    }
}
