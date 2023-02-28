using CerberusGameServer.Networking;
using CommonData.GameServer;
using CommonData.ServerData;
using NovaCoreNetworking;
using Packets;

namespace GameServer.Networking {

    public static class NetworkSend {

        public static void SendClientId(ushort clientId) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_ClientId);
            message.Add(clientId);
            NetworkConfig.server.Send(message, clientId);
        }

        public static void SendToAllInLobby(Lobby lobby, Message message) {
            for (int i = 0; i < lobby.PlayersGameData.Count; i++) {
                NetworkConfig.server.Send(message, lobby.PlayersGameData[i].clientId);
            }
        }

        public static void SendErrorMessage(ushort clientId, ErrorMessage errorMessage) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_ErrorMessage);
            message.Add(errorMessage.errorId);
            message.Add(errorMessage.playerErrorMessage);
            NetworkConfig.server.Send(message, clientId);
        }

        public static void SendPlayerJoinedLobby(ushort joiningPlayer, Lobby lobby) {
            //Goes to the player that has just joined
            for (int i = 0; i < lobby.PlayersGameData.Count; i++) {
                Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerJoinedLobby);
                message.AddString(lobby.PlayersGameData[i].dbPlayer.SteamName);
                message.AddString(lobby.PlayersGameData[i].dbPlayer.SteamID);
                message.AddInt(lobby.PlayersGameData[i].clientId);
                message.AddInt(lobby.PlayersGameData[i].teamId);
                message.AddFloat(lobby.PlayersGameData[i].currentPosition.X);
                message.AddFloat(lobby.PlayersGameData[i].currentPosition.Y);
                message.AddFloat(lobby.PlayersGameData[i].currentPosition.Z);
                message.AddFloat(lobby.PlayersGameData[i].currentRotation.X);
                message.AddFloat(lobby.PlayersGameData[i].currentRotation.Y);
                message.AddFloat(lobby.PlayersGameData[i].currentRotation.Z);
                message.AddFloat(lobby.PlayersGameData[i].currentRotation.W);
                NetworkConfig.server.Send(message, joiningPlayer);
            }

            //Goes to all the other players
            for (int i = 0; i < lobby.PlayersGameData.Count; i++) {
                if (lobby.PlayersGameData[i].clientId != joiningPlayer) {
                    Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerJoinedLobby);
                    message.AddString(lobby.PlayersGameData[i].dbPlayer.SteamName);
                    message.AddString(lobby.PlayersGameData[i].dbPlayer.SteamID);
                    message.AddInt(lobby.PlayersGameData[i].clientId);
                    message.AddInt(lobby.PlayersGameData[i].teamId);
                    message.AddFloat(lobby.PlayersGameData[i].currentPosition.X);
                    message.AddFloat(lobby.PlayersGameData[i].currentPosition.Y);
                    message.AddFloat(lobby.PlayersGameData[i].currentPosition.Z);
                    message.AddFloat(lobby.PlayersGameData[i].currentRotation.X);
                    message.AddFloat(lobby.PlayersGameData[i].currentRotation.Y);
                    message.AddFloat(lobby.PlayersGameData[i].currentRotation.Z);
                    message.AddFloat(lobby.PlayersGameData[i].currentRotation.W);
                    break;
                }
            }
        }

        public static void SendPlayerLeftLobby(Lobby lobby) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerLeftLobby);

            SendToAllInLobby(lobby, message);
        }

        public static void SendPlayerTransformUpdate(Lobby lobby) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerTransformUpdate);
        }

        public static void SendPlayerChoseCharacter(Lobby lobby, string steamId, int chosenCharacter, int chosenSkin) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerChoseCharacter);
            message.Add(steamId);
            message.Add(chosenCharacter);
            message.Add(chosenSkin);
            SendToAllInLobby(lobby, message);
        }

        public static void SendGameStarted(Lobby lobby) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_GameStarted);
            SendToAllInLobby(lobby, message);
        }

        public static void SendGameEnded(Lobby lobby) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_GameEnded);
            SendToAllInLobby(lobby, message);
        }

        public static void SendPlayerTookHealing(Lobby lobby, GamePlayerData playerTookHealing) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerTookHealing);
            message.Add(playerTookHealing.currentHealth);
            SendToAllInLobby(lobby, message);
        }

        public static void SendPlayerTookDamage(Lobby lobby, GamePlayerData playerTookDamage) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerTookDamage);
            message.Add(playerTookDamage.currentHealth);
            SendToAllInLobby(lobby, message);
        }

        public static void SendPlayerDied(Lobby lobby, GamePlayerData deadPlayer, GamePlayerData killingPlayer, List<string> assistPlayers) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerDied);
            message.Add(assistPlayers.Count);

            for (int i = 0; i < assistPlayers.Count; i++) {
                message.Add(assistPlayers[i]);
            }

            message.Add(deadPlayer.dbPlayer.SteamID);
            message.Add(deadPlayer.dbPlayer.SteamName);

            SendToAllInLobby(lobby, message);
        }
    }
}