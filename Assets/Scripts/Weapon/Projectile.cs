using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [Tooltip("Move Speed of Projectile")]
    public float speed;

    [Tooltip("Damage Amount")]
    public int damage;

    [Tooltip("Distance from spawn point to self destruct")]
    public float distance;

    [Tooltip("Enable time based limit")]
    public bool useLifetime = false;

    [EnableIf(nameof(useLifetime))]
    [Tooltip("Time to self destruct")]
    public float lifetime;

    public bool damageOnce = false;

    public UnityEvent onProjectileDestroy;

    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _initialPosition;
    private bool hasDamaged = false;

    private void Awake()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.parent = null;
    }

    /// <summary>
    /// Cache the component references and initialize the default values.
    /// </summary>
    internal void SetInitialSettings()
    {
        if (useLifetime)
        {
            Invoke(nameof(SelfDestruct), lifetime);
        }
        _initialPosition = transform.position;
    }

    /// <summary>
    /// Move in the forward direction.
    /// </summary>
    private void Update()
    {
        if (useLifetime) return;

        if (Vector3.Distance(transform.position, _initialPosition) > distance)
        {
            SelfDestruct();
        }
    }

    /// <summary>
    /// Perform any damage to the collided object and destroy itself.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider collision)
    {
        if (damageOnce && hasDamaged)
        {
            return;
        }

        /*IDamageable damageable;
        if ((damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable) != null)
        {
            damageable.Damage(damage, transform);
            if (damageOnce)
            {
                hasDamaged = true;
            }
            SelfDestruct();
        }*/
    }

    /// <summary>
    /// Destroy itself.
    /// </summary>
    private void SelfDestruct()
    {
        onProjectileDestroy?.Invoke();
        Destroy(gameObject);
    }

    public void ApplyVelocity(Vector3 projectileSpeed)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * 100f,ForceMode.Impulse);
    }
}