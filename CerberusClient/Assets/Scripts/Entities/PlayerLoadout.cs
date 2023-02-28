using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerLoadout : MonoBehaviour {
    public Weapon_SO _primaryWeaponSo;
    public Weapon_SO _secondaryWeaponSo;

    public void CreateScriptableObjects(int? primaryBaseWepId, int? secondaryBaseWepId) {
        _primaryWeaponSo = new();
        _secondaryWeaponSo = new();

        if (primaryBaseWepId != null) {
            int primaryBase = primaryBaseWepId.Value;
            BaseWeapon baseWep = GameManager.instance._baseWeaponDict[primaryBase];

            ShootStyle shootStyle;
            _primaryWeaponSo.weaponID = primaryBase;

            Enum.TryParse(baseWep.ShootStyle, out shootStyle);
            _primaryWeaponSo.shootStyle = shootStyle;

            _primaryWeaponSo.fireRate = baseWep.FireRate;
            _primaryWeaponSo.bulletRange = baseWep.BulletRange;
            _primaryWeaponSo.bulletsPerFire = baseWep.BulletsPerFire;
            _primaryWeaponSo.spreadAmount = baseWep.BulletSpreadAmount;
            _primaryWeaponSo.aimSpreadAmount = baseWep.BulletAimSpreadAmount;
            _primaryWeaponSo.xRecoilAmount = baseWep.XRecoilAmount;
            _primaryWeaponSo.yRecoilAmount = baseWep.YRecoilAmount;
            _primaryWeaponSo.penetrationAmount = baseWep.BulletPenAmount;
            _primaryWeaponSo.damageReductionMultiplier = baseWep.BulletPenDamageReduction;
            _primaryWeaponSo.magazineSize = baseWep.MagazineSize;
            _primaryWeaponSo.damagePerBullet = baseWep.DamagePerBullet;
            _primaryWeaponSo.reloadTime = baseWep.ReloadTime;
            _primaryWeaponSo.weightMultiplier = baseWep.WepWeightModifier;
        }

        if (primaryBaseWepId != null) {
            int secondaryBase = secondaryBaseWepId.Value;
            BaseWeapon baseWep = GameManager.instance._baseWeaponDict[secondaryBase];

            ShootStyle shootStyle;
            _secondaryWeaponSo.weaponID = secondaryBase;

            Enum.TryParse(baseWep.ShootStyle, out shootStyle);
            _secondaryWeaponSo.shootStyle = shootStyle;

            _secondaryWeaponSo.fireRate = baseWep.FireRate;
            _secondaryWeaponSo.bulletRange = baseWep.BulletRange;
            _secondaryWeaponSo.bulletsPerFire = baseWep.BulletsPerFire;
            _secondaryWeaponSo.spreadAmount = baseWep.BulletSpreadAmount;
            _secondaryWeaponSo.aimSpreadAmount = baseWep.BulletAimSpreadAmount;
            _secondaryWeaponSo.xRecoilAmount = baseWep.XRecoilAmount;
            _secondaryWeaponSo.yRecoilAmount = baseWep.YRecoilAmount;
            _secondaryWeaponSo.penetrationAmount = baseWep.BulletPenAmount;
            _secondaryWeaponSo.damageReductionMultiplier = baseWep.BulletPenDamageReduction;
            _secondaryWeaponSo.magazineSize = baseWep.MagazineSize;
            _secondaryWeaponSo.damagePerBullet = baseWep.DamagePerBullet;
            _secondaryWeaponSo.reloadTime = baseWep.ReloadTime;
            _secondaryWeaponSo.weightMultiplier = baseWep.WepWeightModifier;
        }
    }
}