public class BaseCharacterAbility {
    public int AbilityId;
    public int CharacterId;
    public string AbilityName;
    public int Healing;
    public int Damage;
    public int Cooldown;
    public string AbiltyBind;
    public string AbiltyType;
    public int UltimateChargeModifier;

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
}