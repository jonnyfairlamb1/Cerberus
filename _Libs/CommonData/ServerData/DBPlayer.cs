using CommonData.Entities;
using CommonData.PlayerSendData;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CommonData.ServerData {

    public class DBPlayer {

        [JsonPropertyName("steamName")]
        public string SteamName { get; set; }

        [JsonPropertyName("steamID")]
        public string SteamID { get; set; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }

        [JsonPropertyName("accountStanding")]
        public string AccountStanding { get; set; }

        [JsonPropertyName("manualLogout")]
        public bool ManualLogout { get; set; }

        [JsonPropertyName("playerLevel")]
        public int PlayerLevel { get; set; }

        [JsonPropertyName("currency")]
        public int Currency { get; set; }

        [JsonPropertyName("battlepassLevel")]
        public int BattlepassLevel { get; set; }

        [JsonPropertyName("banTimeout")]
        public DateTime BanTimeout { get; set; }

        [JsonPropertyName("weaponLoadouts")]
        public List<WeaponLoadouts> WeaponLoadouts { get; set; }

        [JsonPropertyName("ownedPlayerSkins")]
        public List<OwnedSkinsTuple> OwnedPlayerSkins { get; set; }

        [JsonPropertyName("currentlyEquipedSkin")]
        public List<EquippedSkinsTuple> EquippedSkins { get; set; }

        public DBPlayer(string steamName, string steamID, string ipAddress, string accountStanding, int playerLevel,
            int battlepassLevel, int currency, List<WeaponLoadouts> weaponLoadouts = null, List<OwnedSkinsTuple> ownedPlayerSkins = null,
            List<EquippedSkinsTuple> equippedSkins = null) {
            SteamName = steamName;
            SteamID = steamID;
            IpAddress = ipAddress;
            AccountStanding = accountStanding;
            WeaponLoadouts = weaponLoadouts;
            PlayerLevel = playerLevel;
            BattlepassLevel = battlepassLevel;
            Currency = currency;
            OwnedPlayerSkins = ownedPlayerSkins;
            EquippedSkins = equippedSkins;
        }

        public DBPlayer() {
        }

        public int GetEquippedSkinForCharacterId(int characterId) {
            for (int i = 0; i < EquippedSkins.Count; i++) {
                if (EquippedSkins[i].CharacterId == characterId) return EquippedSkins[i].SkinId;
            }

            return -1;
        }
    }
}