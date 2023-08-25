using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour
{
    public bool debug;
    
#if UNITY_EDITOR
    [ValueDropdown(nameof(GetAllTags))]
#endif
    public List<string> CollisionTags;

    public LayerMask layer;

    public UnityEvent<Collider> onCollisionEnterEvent;
    public UnityEvent onCollisionExitEvent;

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        if (CollisionTags.Count > 0)
        {
            if (!CollisionTags.Contains(other.tag)) return;
        }
        if (( layer & (1 << other.gameObject.layer)) == 0) return;
        
        if (debug)
        {
            print("this.name " + name);
            print("other.name " + other.name);
            print("other.tag " + other.tag);
        }

        onCollisionEnterEvent?.Invoke(other);
    }

#if UNITY_EDITOR
    private string[] GetAllTags()
    {
        return UnityEditorInternal.InternalEditorUtility.tags;
    }
#endif

    private void OnCollisionExit(Collision collision)
    {
        var other = collision.collider;

        if (CollisionTags.Contains(other.tag))
            onCollisionExitEvent?.Invoke();
    }
}