using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEditorManager : MonoBehaviour {
    public static CharacterEditorManager instance;

    public GameObject _buttonPrefab;

    public GameObject _characterContentObject;
    public GameObject _abilityContentObject;
    public GameObject _skinContentObject;

    public List<TMP_Text> _characterStatsTextPrefab;
    public List<TMP_Text> _characterSkinTextPrefab;
    public List<TMP_Text> _characterAbilityTextPrefab;

    private bool _createdFirst = false;

    private void Start() {
        instance = this;
    }

    public void GenerateButton(BaseCharacter baseCharacter) {
        GameObject go = Instantiate(_buttonPrefab) as GameObject;
        go.transform.SetParent(_characterContentObject.transform);

        go.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(baseCharacter));
        go.GetComponentInChildren<TMP_Text>().text = baseCharacter.CharacterName;
        if (!_createdFirst) {
            _createdFirst = true;
            OnButtonClick(baseCharacter);
        }
        Debug.Log("Generated character button");
    }

    public void OnButtonClick(BaseCharacter baseCharacter) {
        for (int i = 0; i < _characterStatsTextPrefab.Count; i++) {
            switch (_characterStatsTextPrefab[i].name) {
                case "CharacterId":
                    _characterStatsTextPrefab[i].text = baseCharacter.CharacterId.ToString();
                    break;

                case "CharacterName":
                    _characterStatsTextPrefab[i].text = baseCharacter.CharacterName.ToString();
                    break;

                case "Health":
                    _characterStatsTextPrefab[i].text = baseCharacter.Health.ToString();
                    break;

                case "RunSpeed":
                    _characterStatsTextPrefab[i].text = baseCharacter.RunSpeed.ToString();
                    break;

                case "FootstepVolume":
                    _characterStatsTextPrefab[i].text = baseCharacter.FootstepVolume.ToString();
                    break;

                default:
                    break;
            }
        }

        GenerateSkinButtons(baseCharacter);
        GenerateAbilitiesButtons(baseCharacter);
    }

    public void GenerateSkinButtons(BaseCharacter baseCharacter) {
        Button[] buttons = _skinContentObject.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++) {
            Destroy(buttons[i].gameObject);
        }

        for (int i = 0; i < baseCharacter.characterSkins.Count; i++) {
            GameObject go = Instantiate(_buttonPrefab) as GameObject;
            go.transform.SetParent(_skinContentObject.transform);

            CharacterSkin skin = baseCharacter.characterSkins[i];

            go.GetComponent<Button>().onClick.AddListener(() => SkinButtonOnClick(skin));
            go.GetComponentInChildren<TMP_Text>().text = skin.SkinName;
        }
    }

    public void SkinButtonOnClick(CharacterSkin skin) {
        for (int i = 0; i < _characterSkinTextPrefab.Count; i++) {
            switch (_characterSkinTextPrefab[i].name) {
                case "SkinId":
                    _characterSkinTextPrefab[i].text = skin.SkinId.ToString();
                    break;

                case "CharacterId":
                    _characterSkinTextPrefab[i].text = skin.CharacterId.ToString();
                    break;

                case "CharacterName":
                    _characterSkinTextPrefab[i].text = skin.CharacterName.ToString();
                    break;

                case "Cost":
                    _characterSkinTextPrefab[i].text = skin.Cost.ToString();
                    break;

                case "IsOwned":
                    _characterSkinTextPrefab[i].text = skin.IsOwned.ToString();
                    break;

                case "IsEquipped":
                    _characterSkinTextPrefab[i].text = skin.IsEquipped.ToString();
                    break;

                default:
                    break;
            }
        }
    }

    public void GenerateAbilitiesButtons(BaseCharacter baseCharacter) {
        Button[] buttons = _abilityContentObject.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++) {
            Destroy(buttons[i].gameObject);
        }

        for (int i = 0; i < baseCharacter.characterAbilities.Count; i++) {
            GameObject go = Instantiate(_buttonPrefab) as GameObject;
            go.transform.SetParent(_abilityContentObject.transform);

            var ability = baseCharacter.characterAbilities[i];

            go.GetComponent<Button>().onClick.AddListener(() => AbilitiesButtonOnClick(ability));
            go.GetComponentInChildren<TMP_Text>().text = ability.AbilityName;
        }
    }

    public void AbilitiesButtonOnClick(BaseCharacterAbility ability) {
        for (int i = 0; i < _characterAbilityTextPrefab.Count; i++) {
            switch (_characterAbilityTextPrefab[i].name) {
                case "AbilityId":
                    _characterAbilityTextPrefab[i].text = ability.AbilityId.ToString();
                    break;

                case "CharacterId":
                    _characterAbilityTextPrefab[i].text = ability.CharacterId.ToString();
                    break;

                case "AbilityName":
                    _characterAbilityTextPrefab[i].text = ability.AbilityName.ToString();
                    break;

                case "Healing":
                    _characterAbilityTextPrefab[i].text = ability.Healing.ToString();
                    break;

                case "Damage":
                    _characterAbilityTextPrefab[i].text = ability.Damage.ToString();
                    break;

                case "Cooldown":
                    _characterAbilityTextPrefab[i].text = ability.Cooldown.ToString();
                    break;

                case "AbilityBind":
                    _characterAbilityTextPrefab[i].text = ability.AbiltyBind.ToString();
                    break;

                case "AbilityType":
                    _characterAbilityTextPrefab[i].text = ability.AbiltyType.ToString();
                    break;

                case "UltimateCharge":
                    _characterAbilityTextPrefab[i].text = ability.UltimateChargeModifier.ToString();
                    break;

                default:
                    break;
            }
        }
    }
}