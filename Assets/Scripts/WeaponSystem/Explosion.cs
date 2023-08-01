#pragma warning disable CS0414

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    public float radius;
    public LayerMask layerMask;

    private bool _drawExplosionGizmo = false;

    #if UNITY_EDITOR

    private bool isFill = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (isFill)
        {
            Gizmos.DrawSphere(transform.position, radius);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
    #endif

    public void Explode()
    {
#if UNITY_EDITOR
        isFill = true;
#endif

       var collidersInRange = Physics.OverlapSphere( transform.position, radius, layerMask);

        foreach( var collidedObject in collidersInRange )
        {
            if( collidedObject == null ) continue;
            /*collidedObject.transform.GetComponent<IDamageable>()?.Damage( damage );*/
        }
    }
}