using CommonData.GameServer;
using System.Collections.Generic;

namespace CommonData.ServerData {

    public enum PlayerRole {
        Player,
        Spectator
    };

    public class Lobby {
        public int LobbyID { get; set; }
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }

        public int MaxPlayers { get; set; }
        public int MaxSpectators { get; set; }
        public GameMaps GameMap { get; set; }
        public GameMode GameMode { get; set; }
        public List<DBPlayer> Players { get; set; }
        public List<DBPlayer> Spectators { get; set; }

        public List<GamePlayerData> PlayersGameData { get; set; } = new();

        public Dictionary<int, DBPlayer[]> LobbyTeams { get; set; }

        public Lobby(int lobbyID, GameMaps gameMap, GameMode gamemode, string serverIp, int serverPort) {
            LobbyID = lobbyID;
            GameMap = gameMap;
            GameMode = gamemode;

            Players = new();
            Spectators = new();
            ServerIp = serverIp;
            ServerPort = serverPort;
            MaxPlayers = GameMode.MaxPlayers;
            MaxSpectators = GameMode.MaxSpectators;

            LobbyTeams = BuildTeamsDictionary(gamemode);
        }

        public Lobby() {
        }

        private static Dictionary<int, DBPlayer[]> BuildTeamsDictionary(GameMode gamemode) {
            Dictionary<int, DBPlayer[]> teams = new();

            for (int x = 0; x < gamemode.AmountOfTeams; x++) {
                DBPlayer[] team = new DBPlayer[gamemode.TeamSize];
                teams.Add(x, team);
            }

            return teams;
        }
    }
}