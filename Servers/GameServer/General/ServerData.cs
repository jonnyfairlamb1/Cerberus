using CommonData.PlayerSendData;
using CommonData.ServerData;

namespace GameServer.General {

    public static class ServerData {
        public static Dictionary<int, ErrorMessage> _errorMessages;
        public static GameServerData _gameServer;
        public static Dictionary<int, BaseCharacter> _characters;

        public static Dictionary<int, LobbyManager> _lobbyMangagers { get; set; } = new();

        public static Dictionary<string, int> _playerLobbies = new();

        public static LobbyManager GetLobbyOfPlayer(string steamId) {
            int lobbyId = _playerLobbies[steamId];

            return _lobbyMangagers[lobbyId];
        }
    }
}