using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {
    public string steamName;
    public string steamId;
    public int playerId;
    public int teamId;

    public BaseCharacter currentCharacter;

    public void UpdateTextFields(TMP_Text[] textfields) {
        for (int i = 0; i < textfields.Length; i++) {
            if (textfields[i].name == "PlayerNameLabelTxt") textfields[i].text = steamName;
            if (textfields[i].name == "PlayerCharacterNameLabelTxt") textfields[i].text = currentCharacter.CharacterName;
        }
    }
}