using CommonData.DTOs;
using System.Collections.Generic;

namespace CommonData.ServerData {

    public class GameServerData {
        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; }
        public int GameServerId { get; set; }

        public Dictionary<int, Lobby> Lobbies { get; set; } = new();

        public GameServerData(GameServerDataDTO gameServerDataDTO) {
            Lobbies = new();

            IpAddress = gameServerDataDTO.IpAddress;
            Port = gameServerDataDTO.Port;
            GameServerId = gameServerDataDTO.GameServerId;

            Lobbies = gameServerDataDTO.Lobbies;
        }

        public GameServerData(string ipAddress, int port, int gameServerId, int amountOfLobbies, List<GameMaps> maps, List<GameMode> gameModes) {
            IpAddress = ipAddress;
            Port = port;
            GameServerId = gameServerId;

            for (int i = 0; i < amountOfLobbies; i++) {
                Lobbies.Add(i, new(i, maps[i], gameModes[i], ipAddress, port));
            }
            //Lobbies = lobbies;
        }

        public GameServerData() {
        }
    }
}