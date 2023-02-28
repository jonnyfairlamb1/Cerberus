using Assets.Scripts.Entities;
using System.Collections.Generic;

namespace CerberusClient.Network.Data {

    public class GameServerData {
        public int lobbyID;
        public string serverIp;
        public int serverPort;

        public string abbreviation;
        public string mapName;
        public string gameMode;
        public bool gameStarted = false;
        public List<GamePlayerData> gamePlayerData;
    }
}