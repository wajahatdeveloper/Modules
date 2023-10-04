using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class WeaponBase : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnVector;
    public int animatorWeaponType = 0;
    [Tooltip("Current Ammo")]
    public int ammo;
    [Tooltip("Ammo On Reload")]
    public int magazineSize;
    [Tooltip("Projectiles per Second")]
    public int fireRate;

    [SerializeField, Layer]
    public int projectileLayer;

    [Header("Reload")]
    [Tooltip("Time to Replenish Ammo in Seconds")]
    public float reloadTime;
    [Tooltip("Will not auto reload if set to false")]
    public bool allowReload = true;

    [Header("Damage Override")]
    public bool isDamageOverride = false;
    public int damageOverride;

    [Header("Range Override")]
    public bool isRangeOverride = false;
    public float rangeRadius;

    [Header("VariantId")] public int upgradeLevel;

    public UnityEvent onOutOfAmmo;

    private bool canShoot;
    private bool isReloading;
    private Transform dynamicParent;

    //bool doShoot = false;

    public bool CanShoot
    {
        get => canShoot;
        set
        {
            canShoot = value;
        }
    }

    protected void OnEnableHandler()
    {
        dynamicParent = GameObject.FindWithTag("Dynamic")?.transform;
        CanShoot = true;
    }

    [Button(ButtonStyle.Box,Name = nameof(Shoot))]
    public void Shoot(bool useIK = true, Action callback = null)
    {
        if (!CanShoot) { return; }
        if (isReloading) { return; }

        if (ammo <= 0)
        {
            if( allowReload )
            {
                Reload();
            }
            else
            {
                onOutOfAmmo?.Invoke();
            }
            return;
        }
        if(gameObject.activeInHierarchy) {
        StartCoroutine(Routine_Shoot(callback));

        }
    }

    void ShootProjectile()
    {
        var projectileObject = Instantiate(projectilePrefab, projectileSpawnVector, false);

        projectileObject.layer = projectileLayer;

        var projectile = projectileObject.GetComponent<Projectile>();

        if (isDamageOverride)
        {
            projectile.damage = damageOverride;
        }

        if (isRangeOverride)
        {
            projectile.useLifetime = false;
            projectile.distance = rangeRadius;
        }

        projectile.SetInitialSettings();

        var forward = projectileSpawnVector.forward;
        projectile.ApplyVelocity(forward * projectile.speed);

        ammo--;
    }

    private IEnumerator Routine_Shoot( Action callback)
    {
        //doShoot = false;
        CanShoot = false;

        yield return new WaitForEndOfFrame();

        ShootProjectile();

        // Fire Rate Handler
        float timeToNextShoot = (1f / fireRate);

        callback?.Invoke();

        yield return new WaitForSeconds(timeToNextShoot);

        CanShoot = true;
    }

    [Button(ButtonStyle.Box,Name = nameof(Reload))]
    public void Reload()
    {
        isReloading = true;

        StartCoroutine(Routine_Reload());
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }



    private IEnumerator Routine_Reload()
    {
        yield return new WaitForSeconds(reloadTime);

        ammo = magazineSize;
        isReloading = false;
    }
}