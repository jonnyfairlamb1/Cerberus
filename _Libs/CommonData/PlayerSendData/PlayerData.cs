using System.Text.Json.Serialization;

namespace CommonData.PlayerSendData {

    public class PlayerData {

        [JsonPropertyName("steamName")]
        public string SteamName { get; set; }

        [JsonPropertyName("steamID")]
        public string SteamID { get; set; }

        [JsonPropertyName("playerLevel")]
        public int PlayerLevel { get; set; }

        public PlayerData(string steamName, string steamID, int playerLevel) {
            SteamName = steamName;
            SteamID = steamID;
            PlayerLevel = playerLevel;
        }

        public PlayerData() {
        }
    }
}