using NovaCore;
using Packets;

public static class NetworkSend {

    public static void SendLogin(string steamId, string steamName) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientPackets.C_Login);
        message.AddString(steamName);
        message.AddString(steamId);
        NetworkManager.instance.Client.Send(message);
    }
}