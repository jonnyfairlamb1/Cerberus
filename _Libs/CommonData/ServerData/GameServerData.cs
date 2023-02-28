using System.Collections.Generic;

namespace CommonData.ServerData {

    public class GameServerData {
        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; }
        public int GameServerId { get; set; }

        public Dictionary<int, Lobby> Lobbies { get; set; }

        public GameServerData(string ipAddress, int port, int gameServerId, int amountOfLobbies, List<GameMaps> gameMap, List<GameMode> gameModes) {
            Lobbies = new();

            IpAddress = ipAddress;
            Port = port;
            GameServerId = gameServerId;

            for (int i = 0; i < amountOfLobbies; i++) {
                Lobbies.Add(i, new Lobby(i, gameMap[i], gameModes[i], ipAddress, port));
            }
        }

        public GameServerData() {
        }
    }
}