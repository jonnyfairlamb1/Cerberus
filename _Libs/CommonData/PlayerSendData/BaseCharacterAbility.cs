namespace CommonData.PlayerSendData {

    public class BaseCharacterAbility {
        public int AbilityId { get; set; }
        public int CharacterId { get; set; }
        public string AbilityName { get; set; }
        public int Healing { get; set; }
        public int Damage { get; set; }
        public int Cooldown { get; set; }
        public string AbiltyBind { get; set; }
        public string AbiltyType { get; set; }
        public int UltimateChargeModifier { get; set; }

        public BaseCharacterAbility(int abilityId, int characterId, string abilityName, int healing, int damage, int cooldown,
            string abilityBind, string abilityType, int ultimateCharge) {
            AbilityId = abilityId;
            CharacterId = characterId;
            AbilityName = abilityName;
            Healing = healing;
            Damage = damage;
            Cooldown = cooldown;
            AbiltyBind = abilityBind;
            AbiltyType = abilityType;
            UltimateChargeModifier = ultimateCharge;
        }

        public BaseCharacterAbility() {
        }
    }
}