using System;
using UnityEngine;
using System.Collections;
 
public class Behaviour_LookAtCamera : MonoBehaviour
{
    private Camera cameraToLookAt;

    private void OnEnable()
    {
        cameraToLookAt = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v);
    }
}