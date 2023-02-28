using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BaseWeapon {
    public int WeaponId;
    public string LoadoutSlot;
    public string WeaponType;
    public string DisplayName;
    public string AssetName;

    public string ShootStyle;
    public float FireRate;
    public int BulletRange;
    public int BulletsPerFire;
    public float BulletSpreadAmount;
    public float BulletAimSpreadAmount;
    public int XRecoilAmount;
    public int YRecoilAmount;
    public float BulletPenAmount;
    public float BulletPenDamageReduction;
    public int MagazineSize;
    public float DamagePerBullet;
    public float ReloadTime;
    public float WepWeightModifier;

    public BaseWeapon(int weaponId, string loadoutSlot, string weaponType, string displayName, string assetName, string shootStyle,
        float fireRate, int bulletRange, int bulletsPerFire, float bulletSpreadAmount, float bulletAimSpreadAmount, int xRecoilAmount,
        int yRecoilAmount, float bulletPenAmount, float bulletPenDamageReduction, int magazineSize, float damagePerBullet, float reloadTime, float wepWeightModifier) {
        WeaponId = weaponId;
        LoadoutSlot = loadoutSlot;
        WeaponType = weaponType;
        DisplayName = displayName;
        AssetName = assetName;
        ShootStyle = shootStyle;
        FireRate = fireRate;
        BulletRange = bulletRange;
        BulletsPerFire = bulletsPerFire;
        BulletSpreadAmount = bulletSpreadAmount;
        BulletAimSpreadAmount = bulletAimSpreadAmount;
        XRecoilAmount = xRecoilAmount;
        YRecoilAmount = yRecoilAmount;
        BulletPenAmount = bulletPenAmount;
        BulletPenDamageReduction = bulletPenDamageReduction;
        MagazineSize = magazineSize;
        DamagePerBullet = damagePerBullet;
        ReloadTime = reloadTime;
        WepWeightModifier = wepWeightModifier;
    }
}