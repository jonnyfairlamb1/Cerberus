namespace CommonData.ServerData {

    public class CharacterSkins {
        public int SkinId { get; set; }
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string SkinName { get; set; }
        public int Cost { get; set; }

        public CharacterSkins(int skinId, int characterId, string characterName, string skinName, int cost) {
            SkinId = skinId;
            CharacterId = characterId;
            CharacterName = characterName;
            SkinName = skinName;
            Cost = cost;
        }
    }
}