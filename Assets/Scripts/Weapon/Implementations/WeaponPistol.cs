using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponPistol : WeaponBase , IFetchData
{
    private float _fireRate;
    private float _bulletDamage;
    private float _reloadTime;
    private float _rangeRadius;
    private float _magazineSize;
    
    private void OnEnable()
    {
        OnEnableHandler();
        
        ///FetchData();
        //ApplyData();
    }

    public void ApplyData()
    {
        reloadTime = _reloadTime;
        
        fireRate = (int)_fireRate;
        magazineSize = (int)_magazineSize;

        isDamageOverride = true;
        damageOverride = (int)_bulletDamage;

        isRangeOverride = true;
        rangeRadius = _rangeRadius;
    }
    
    public void FetchData()
    {
        var data = DataManager.Stats.weaponStats.pistol.GetStatsOfLevel((byte)upgradeLevel);
        
        _fireRate = data.fireRate;
        _bulletDamage = data.bulletDamage;
        _reloadTime = data.reloadTime;
        _rangeRadius = data.rangeRadius;
        _magazineSize = data.magazineSize;
    }
}