public class CharacterSkin {
    public int SkinId;
    public int CharacterId;
    public string CharacterName;
    public string SkinName;
    public int Cost;
    public bool IsOwned = false;
    public bool IsEquipped = false;

    public CharacterSkin(int skinId, int characterId, string characterName, string skinName, int cost) {
        SkinId = skinId;
        CharacterId = characterId;
        CharacterName = characterName;
        SkinName = skinName;
        Cost = cost;
    }
}