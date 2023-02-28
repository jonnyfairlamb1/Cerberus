using System.Text.Json.Serialization;

namespace CommonData.PlayerSendData {

    public class PlayerData {

        [JsonPropertyName("steamName")]
        public string SteamName { get; set; }

        [JsonPropertyName("steamID")]
        public string SteamID { get; set; }

        [JsonPropertyName("playerLevel")]
        public int PlayerLevel { get; set; }

        [JsonPropertyName("battlepassLevel")]
        public int BattlepassLevel { get; set; }

        [JsonPropertyName("currencyAmount")]
        public int CurrencyAmount { get; set; }

        public PlayerData(string steamName, string steamID, int playerLevel, int battlePassLevel, int currencyAmount) {
            SteamName = steamName;
            SteamID = steamID;
            PlayerLevel = playerLevel;
            BattlepassLevel = battlePassLevel;
            CurrencyAmount = currencyAmount;
        }

        public PlayerData() {
        }
    }
}