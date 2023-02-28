namespace CommonData.PlayerSendData {

    public class BaseWeapon {
        public int WeaponId { get; set; }
        public string LoadoutSlot { get; set; }
        public string WeaponType { get; set; }
        public string DisplayName { get; set; }
        public string AssetName { get; set; }

        public string ShootStyle { get; set; }
        public float FireRate { get; set; }
        public int BulletRange { get; set; }
        public int BulletsPerFire { get; set; }
        public float BulletSpreadAmount { get; set; }
        public float BulletAimSpreadAmount { get; set; }
        public int XRecoilAmount { get; set; }
        public int YRecoilAmount { get; set; }
        public float BulletPenAmount { get; set; }
        public float BulletPenDamageReduction { get; set; }
        public int MagazineSize { get; set; }
        public float DamagePerBullet { get; set; }
        public float ReloadTime { get; set; }
        public float WepWeightModifier { get; set; }

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

        public BaseWeapon() {
        }
    }
}