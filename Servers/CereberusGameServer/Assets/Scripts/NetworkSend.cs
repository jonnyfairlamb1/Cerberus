

using System.Collections.Generic;
using NovaCore;
using NovaCore.Utils;
using Packets;
using UnityEditor.PackageManager;

namespace Assets.Scripts {
    public static class NetworkSend {

        public static void SendToAllInLobby(Message message)
        {
            foreach (var player in GameManager.Instance.PlayerList)
            {
                NetworkManager.Instance.Server.Send(message, player.Value.PlayerId);
            }
        }

        public static void SendClientId(ushort clientId, int currentGameState, int countDownTimerRemaining) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_ClientId);
            message.Add(clientId);
            message.Add(currentGameState);
            message.Add(countDownTimerRemaining);
            NetworkManager.Instance.Server.Send(message, clientId);
        }

        public static void PlayerJoinedGame(List<string> players)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerJoinedLobby);
            message.Add(players.Count);

            foreach (var player in players)
            {
                message.Add(player);
            }
            SendToAllInLobby(message);
        }

        public static void GameStarted(Dictionary<ushort, Player> playerData, string mapName)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_GameStarted);
            message.Add(mapName);
            SendToAllInLobby(message);
        }

        public static void SendPlayerData(Dictionary<ushort, Player> playerData)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)GameServerPackets.GS_PlayerGameData);
            message.Add(playerData.Count);

            foreach (var player in playerData) {
                message.Add(player.Value.SteamId);
                message.Add(player.Value.SteamName);
                message.Add(player.Value.TeamId);
                //position
                message.Add(0f);
                message.Add(0f);
                message.Add(0f);
                //rotation
                message.Add(0f);
                message.Add(0f);
                message.Add(0f);
                message.Add(0f);
            }

            SendToAllInLobby(message);
        }

        public static void GameEnded()
        {

        }

    }
}
