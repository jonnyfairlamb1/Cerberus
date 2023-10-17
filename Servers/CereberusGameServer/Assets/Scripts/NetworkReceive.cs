using System.Collections.Generic;
using NovaCore;
using Packets;
using NovaCore.Utils;

namespace Assets.Scripts {
    public class NetworkReceive {

        [MessageHandler((ushort)ClientPackets.C_Login)]
        private static void Packet_LoginConfirmed(ushort fromClientId, Message message)
        {
            string playerSteamId = message.GetString();
            string playerSteamName = message.GetString();
            GameManager.Instance.PlayerList[fromClientId].SteamId = playerSteamId;
            GameManager.Instance.PlayerList[fromClientId].SteamName = playerSteamName;

            List<string> playerNames = new();

            foreach (var player in GameManager.Instance.PlayerList)
            {
                playerNames.Add(player.Value.SteamName);

            }

            NetworkSend.PlayerJoinedGame(playerNames);
        }


        [MessageHandler((ushort)ClientPackets.C_GameSceneLoaded)]
        private static void Packet_PlayerLoadedScene(ushort fromClientId, Message message)
        {
            GameManager.Instance.PlayerLoadedGameScene();
        }
    }
}
