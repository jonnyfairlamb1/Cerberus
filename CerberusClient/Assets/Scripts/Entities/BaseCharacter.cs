using System.Collections.Generic;

public class BaseCharacter {
    public int CharacterId;
    public string CharacterName;
    public int Health;
    public int RunSpeed;
    public float FootstepVolume;

    public List<BaseCharacterAbility> characterAbilities = new();
    public List<CharacterSkin> characterSkins = new();

    public BaseCharacter(int characterId, string characterName, int health, int runSpeed, float footstepVolume) {
        CharacterId = characterId;
        CharacterName = characterName;
        Health = health;
        RunSpeed = runSpeed;
        FootstepVolume = footstepVolume;
    }
}