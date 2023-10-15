using CerberusLoginServer.General;
using CommonData.Entities;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using LoginServer.Proxy;
using NovaCore;
using Packets;

namespace CerberusLoginServer.Networking;

public static class NetworkSend {

    public static void Send_ErrorMessage(ushort clientId, ErrorMessage msg) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_ErrorMessage);
        message.Add(msg.errorId);
        message.Add(msg.playerErrorMessage);
        NetworkConfig.server.Send(message, clientId);
    }

    public static void Send_LoginConfirmed(ushort clientId, string playerObject) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)LoginServerPackets.LS_LoginConfirmed);
        message.AddString(playerObject);
        NetworkConfig.server.Send(message, clientId);
    }
}