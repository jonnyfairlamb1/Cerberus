using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEditorManager : MonoBehaviour {
    public static WeaponEditorManager instance;

    public GameObject _buttonPrefab;

    public GameObject _contentObject;

    public List<TMP_Text> _weaponStatsTextPrefab;

    private bool _createdFirst = false;

    private void Start() {
        instance = this;
    }

    public void GenerateButton(BaseWeapon baseWeapon) {
        GameObject go = Instantiate(_buttonPrefab) as GameObject;
        go.transform.SetParent(_contentObject.transform);

        go.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(baseWeapon));
        go.GetComponentInChildren<TMP_Text>().text = baseWeapon.AssetName;
        if (!_createdFirst) {
            _createdFirst = true;
            OnButtonClick(baseWeapon);
        }
    }

    public void OnButtonClick(BaseWeapon baseWeapon) {
        for (int i = 0; i < _weaponStatsTextPrefab.Count; i++) {
            switch (_weaponStatsTextPrefab[i].name) {
                case "WeaponId":
                    _weaponStatsTextPrefab[i].text = baseWeapon.WeaponId.ToString();
                    break;

                case "LoadoutSlot":
                    _weaponStatsTextPrefab[i].text = baseWeapon.LoadoutSlot.ToString();
                    break;

                case "WeaponType":
                    _weaponStatsTextPrefab[i].text = baseWeapon.WeaponType.ToString();
                    break;

                case "DisplayName":
                    _weaponStatsTextPrefab[i].text = baseWeapon.DisplayName.ToString();
                    break;

                case "AssetName":
                    _weaponStatsTextPrefab[i].text = baseWeapon.AssetName.ToString();
                    break;

                case "ShootStyle":
                    _weaponStatsTextPrefab[i].text = baseWeapon.ShootStyle.ToString();
                    break;

                case "FireRate":
                    _weaponStatsTextPrefab[i].text = baseWeapon.FireRate.ToString();
                    break;

                case "BulletRange":
                    _weaponStatsTextPrefab[i].text = baseWeapon.BulletRange.ToString();
                    break;

                case "BulletsPerFire":
                    _weaponStatsTextPrefab[i].text = baseWeapon.BulletsPerFire.ToString();
                    break;

                case "BulletSpreadAmount":
                    _weaponStatsTextPrefab[i].text = baseWeapon.BulletSpreadAmount.ToString();
                    break;

                case "BulletAimSpreadAmount":
                    _weaponStatsTextPrefab[i].text = baseWeapon.BulletAimSpreadAmount.ToString();
                    break;

                case "XRecoilAmount":
                    _weaponStatsTextPrefab[i].text = baseWeapon.XRecoilAmount.ToString();
                    break;

                case "YRecoilAmount":
                    _weaponStatsTextPrefab[i].text = baseWeapon.YRecoilAmount.ToString();
                    break;

                case "BulletPenAmount":
                    _weaponStatsTextPrefab[i].text = baseWeapon.BulletPenAmount.ToString();
                    break;

                case "BulletPenDamageReduction":
                    _weaponStatsTextPrefab[i].text = baseWeapon.BulletPenDamageReduction.ToString();
                    break;

                case "MagazineSize":
                    _weaponStatsTextPrefab[i].text = baseWeapon.MagazineSize.ToString();
                    break;

                case "DamagePerBullet":
                    _weaponStatsTextPrefab[i].text = baseWeapon.DamagePerBullet.ToString();
                    break;

                case "ReloadTime":
                    _weaponStatsTextPrefab[i].text = baseWeapon.ReloadTime.ToString();
                    break;

                case "WepWeightModifier":
                    _weaponStatsTextPrefab[i].text = baseWeapon.WepWeightModifier.ToString();
                    break;

                default:
                    break;
            }
        }
    }
}