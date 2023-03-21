using System;
using System.Collections.Generic;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Allows the Attached Object to Use and Handle All Weapons
/// </summary>
public class WeaponHandler : MonoBehaviour
{
    public List<WeaponBase> availableWeapons = new List<WeaponBase>();
    [SuffixLabel("Optional")] public AimIK aimIK;

    public event Action<WeaponBase> onWeaponChange;     // param: newWeapon

    [Header("Debug Variables")]
    [SerializeField] private AnimatorControl animatorControl;
    [SerializeField] private WeaponBase currentWeapon;
    [SerializeField] private int currentWeaponIndex;


    private void OnEnable()
    {
        animatorControl = GetComponent<AnimatorControl>();
        SelectFirstWeapon();
    }

    public WeaponBase GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void SetCurrentWeapon(int weaponIndex)
    {
        if( currentWeapon != null )
        {
            currentWeapon.GetGameObject().SetActive(false);
        }

        currentWeapon = availableWeapons[weaponIndex];
        currentWeapon.animatorControl = animatorControl;
        currentWeapon.GetGameObject().SetActive(true);
        currentWeaponIndex = weaponIndex;

        if (aimIK != null)
        {
            aimIK.solver.transform = currentWeapon.projectileSpawnVector;
        }

        onWeaponChange?.Invoke(currentWeapon);
    }

    public void SetTemporaryWeapon(WeaponBase tempWeapon)
    {
        if( currentWeapon != null )
        {
            currentWeapon.GetGameObject().SetActive(false);
        }
        currentWeapon = tempWeapon;
        onWeaponChange?.Invoke(currentWeapon);
    }

    public void RestoreWeapon()
    {
        SetCurrentWeapon(currentWeaponIndex);
    }

    [Button]
    public void SelectFirstWeapon()
    {
        if (availableWeapons.Count <= 0)
        {
            Debug.Log("Weapon Handler: No Weapons to select",gameObject);
            return;
        }

        currentWeaponIndex = 0;
        SetCurrentWeapon(currentWeaponIndex);
    }

    [Button]
    public void SelectNextWeapon()
    {
        if (availableWeapons.Count <= 1)
        {
            Debug.Log("Weapon Handler: No Next Weapons to select",gameObject);
            return;
        }

        var nextWeaponIndex = ++currentWeaponIndex;

        if( nextWeaponIndex >= availableWeapons.Count )
        {
            nextWeaponIndex = availableWeapons.Count - 1;
        }
        
        SetCurrentWeapon(nextWeaponIndex);
    }
    
    /// <summary>
    /// Called From SendMessage (Behaviour Tree)
    /// </summary>
    public void Shoot()
    {
        //Debugger.Log("Shoot message received");
        
        currentWeapon.Shoot();
    }
}