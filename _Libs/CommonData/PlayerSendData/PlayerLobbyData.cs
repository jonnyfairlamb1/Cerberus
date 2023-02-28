namespace CommonData.PlayerSendData {

    public class PlayerLobbyData {
        public int LobbyID { get; set; }
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }
        public string MapName { get; set; }
        public string Abbreviation { get; set; }
        public string GameMode { get; set; }

        public PlayerLobbyData(int lobbyID, string serverIp, int serverPort, string mapName, string abbreviation, string gameMode) {
            LobbyID = lobbyID;
            ServerIp = serverIp;
            ServerPort = serverPort;
            MapName = mapName;
            Abbreviation = abbreviation;
            GameMode = gameMode;
        }
    }
}