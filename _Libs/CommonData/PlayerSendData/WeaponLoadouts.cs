using System.Text.Json.Serialization;

namespace CommonData.PlayerSendData {

    public class WeaponLoadouts {

        [JsonPropertyName("loadoutID")]
        public int LoadoutID { get; set; }

        [JsonPropertyName("owningPlayerID")]
        public string OwningPlayerID { get; set; }

        [JsonPropertyName("loadoutName")]
        public string LoadoutName { get; set; }

        [JsonPropertyName("customPrimaryID")]
        public int? CustomPrimaryID { get; set; }

        [JsonPropertyName("customSecondaryID")]
        public int? CustomSecondaryID { get; set; }

        [JsonPropertyName("primaryWeapon")]
        public CustomWeapon PrimaryWeapon { get; set; }

        [JsonPropertyName("secondaryWeapon")]
        public CustomWeapon SecondaryWeapon { get; set; }

        public WeaponLoadouts(int loadoutID, string owningPlayerID, string loadoutName, int? customPrimaryID, int? customSecondaryID, CustomWeapon primaryWeapon, CustomWeapon secondaryWeapon) {
            LoadoutID = loadoutID;
            OwningPlayerID = owningPlayerID;
            LoadoutName = loadoutName;
            CustomPrimaryID = customPrimaryID;
            CustomSecondaryID = customSecondaryID;
            PrimaryWeapon = primaryWeapon;
            SecondaryWeapon = secondaryWeapon;
        }

        public WeaponLoadouts() {
        }
    }
}