
using NovaCore;
using Packets;
using Steamworks;

namespace Assets.Scripts.Network.GameServer {
    public static class GameServerSend {

        public static void SendLogin(string steamId, string steamName)
        {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientPackets.C_Login);
            message.AddString(steamId);
            message.AddString(steamName);
            NetworkManager.instance.Client.Send(message);
        }

        public static void SendSceneLoaded()
        {
            NetworkManager.instance.Client.Send(Message.Create(MessageSendMode.Reliable, (ushort)ClientPackets.C_GameSceneLoaded));
        }
    }
}
