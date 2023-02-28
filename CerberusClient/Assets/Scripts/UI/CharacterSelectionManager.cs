using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour {
    public GameObject _buttonPrefab;
    public GameObject _characterContentObject;
    public GameObject _characterSelectionUi;

    public void Start() {
        for (int i = 0; i < GameManager.instance._characters.Count; i++) {
            GenerateButton(GameManager.instance._characters.ElementAt(i).Value);
        }
        GameManager.instance._characterSelectionUI = _characterSelectionUi;
    }

    public void GenerateButton(BaseCharacter baseCharacter) {
        GameObject go = Instantiate(_buttonPrefab) as GameObject;
        go.transform.SetParent(_characterContentObject.transform);

        go.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(baseCharacter));
        go.GetComponentInChildren<TMP_Text>().text = baseCharacter.CharacterName;
    }

    public void OnButtonClick(BaseCharacter baseCharacter) {
        NetworkSend.SendChoseCharacterToGameServer(baseCharacter);
    }
}