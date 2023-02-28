using System.Text.Json.Serialization;

namespace CommonData.PlayerSendData {

    public class CustomWeapon {

        [JsonPropertyName("customWeaponID")]
        public int? CustomWeaponID { get; set; }

        [JsonPropertyName("loadoutSlot")]
        public string LoadoutSlot { get; set; }

        [JsonPropertyName("weaponID")]
        public int? WeaponID { get; set; }

        [JsonPropertyName("owningPlayerID")]
        public string OwningPlayerID { get; set; }

        [JsonPropertyName("barrelAttachmentID")]
        public int? BarrelAttachmentID { get; set; }

        [JsonPropertyName("scopeAttachmentID")]
        public int? ScopeAttachmentID { get; set; }

        [JsonPropertyName("gripAttachmentID")]
        public int? GripAttachmentID { get; set; }

        [JsonPropertyName("railAttachmentID")]
        public int? RailAttachmentID { get; set; }

        [JsonPropertyName("midBarrelAttachmentID")]
        public int? MidBarrelAttachmentID { get; set; }

        public CustomWeapon(int customWeaponID, string loadoutSlot, int weaponID, string owningPlayerID,
            int? barrelAttachmentID, int? scopeAttachmentID, int? gripAttachmentID, int? railAttachmentID, int? midBarrelAttachmentID) {
            CustomWeaponID = customWeaponID;
            LoadoutSlot = loadoutSlot;
            WeaponID = weaponID;
            OwningPlayerID = owningPlayerID;
            BarrelAttachmentID = barrelAttachmentID;
            ScopeAttachmentID = scopeAttachmentID;
            GripAttachmentID = gripAttachmentID;
            RailAttachmentID = railAttachmentID;
            MidBarrelAttachmentID = midBarrelAttachmentID;
        }

        public CustomWeapon() {
        }
    }
}