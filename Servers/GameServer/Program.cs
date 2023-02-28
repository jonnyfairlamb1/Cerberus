using CerberusGameServer.Networking;
using GameServer.General;
using NovaCoreNetworking.Utils;

namespace CerberusGameServer {

    internal class Program {
        private static readonly ushort _portNumber = 7778;
        private static int _maxPlayers;
        private static readonly int _numberOfLobbies = 1;

        public static Dictionary<int, string> _errorMessages;

        private static void Main() {
            Console.Title = "Cerberus Game Server";

            NovaCoreLogger.Initialize(Console.WriteLine, true);

            ServerData._errorMessages = HttpRequests.GetErrorMessages().Result;
            ServerData._characters = HttpRequests.GetCharacterData().Result;
            ServerData._gameServer = HttpRequests.RegisterServerAsync("127.0.0.1", _portNumber, _numberOfLobbies).Result;

            for (int i = 0; i < ServerData._gameServer.Lobbies.Count; i++) {
                _maxPlayers = _maxPlayers += ServerData._gameServer.Lobbies[i].MaxPlayers;
                ServerData._lobbyMangagers.Add(ServerData._gameServer.Lobbies[i].LobbyID, new(ServerData._gameServer.Lobbies[i]));
                NovaCoreLogger.Log(LogType.Debug, $"Started lobby on map: {ServerData._gameServer.Lobbies[i].GameMap.MapName} with gamemode : {ServerData._gameServer.Lobbies[i].GameMode.GameModeName}");
            }

            NetworkConfig.InitializeServerAsync(_portNumber, (ushort)_maxPlayers);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(NetworkConfig.CloseServer);
        }
    }
}