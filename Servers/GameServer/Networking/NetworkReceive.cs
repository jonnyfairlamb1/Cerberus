using CerberusGameServer.Networking;
using CommonData.GameServer;
using CommonData.ServerData;
using GameServer.General;
using NovaCore;
using Packets;

namespace GameServer.Networking {

    public static class NetworkReceive {

        [MessageHandler((ushort)ClientPackets.C_LobbyId)]
        private static void Packet_LobbyId(ushort fromClientId, Message message) {
            int lobbyId = message.GetInt();
            string steamId = message.GetString();
            Console.WriteLine(lobbyId + "   :   " + steamId);

            DBPlayer? playerData = HttpRequests.GetPlayerData(steamId).Result;

            if (playerData == null) {
                NetworkSend.SendErrorMessage(fromClientId, ServerData._errorMessages[10000]);
                throw new Exception("Add error to the database");
            }

            GamePlayerData gamePlayerData = ServerData._lobbyMangagers[lobbyId].NewPlayerJoinedLobby(fromClientId, playerData);

            if (gamePlayerData == null) {
                NetworkSend.SendErrorMessage(fromClientId, ServerData._errorMessages[10001]);
                return;
            }

            ServerData._playerLobbies.Add(playerData.SteamID, lobbyId);
            //Send to all in lobby
            NetworkSend.SendPlayerJoinedLobby(fromClientId, ServerData._gameServer.Lobbies[lobbyId]);
        }

        [MessageHandler((ushort)ClientPackets.C_PlayerChoseCharacter)]
        private static void Packet_PlayerChoseCharacter(ushort fromClientId, Message message) {
            string steamId = message.GetString();
            int characterId = message.GetInt();

            var lobbyManager = ServerData.GetLobbyOfPlayer(steamId);
            lobbyManager.PlayerChoseCharacter(steamId, characterId);
        }

        [MessageHandler((ushort)ClientPackets.C_PlayerChoseLoadout)]
        private static void Packet_PlayerChoseLoadout(ushort fromClientId, Message message) {
            //TODO: implement player chose loadout
            throw new NotImplementedException();
        }
    }
}