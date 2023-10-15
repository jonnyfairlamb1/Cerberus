using System;
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

        [JsonPropertyName("banTimeout")]
        public DateTime BanTimeout { get; set; }

        public DBPlayer(string steamName, string steamID, string ipAddress, string accountStanding, int playerLevel) {
            SteamName = steamName;
            SteamID = steamID;
            IpAddress = ipAddress;
            AccountStanding = accountStanding;
            PlayerLevel = playerLevel;
        }

        public DBPlayer() {
        }
    }
}