using CerberusLoginServer.General;
using CommonData.Extensions;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using LoginServer.Proxy;
using NovaCore;
using NovaCore.Utils;
using Packets;

namespace CerberusLoginServer.Networking;

public static class NetworkReceive {

    [MessageHandler((ushort)ClientPackets.C_Login)]
    private static void Packet_Login(ushort fromClientId, Message message) {
        NovaCoreLogger.Log(LogType.Debug, "Received Login Message");

        string steamName = message.GetString();
        string steamID = message.GetString();

        DBPlayer? player = HttpRequests.PlayerLoginAsync(steamName, steamID, NetworkConfig.GetIPAddress(fromClientId)).Result;

        if (player == null) {
            NetworkSend.Send_ErrorMessage(fromClientId, DataProxy._errorMessages[10002]);
        } else {
            NetworkManager._clientList.Add(fromClientId, player);

            PlayerData playerData = player.ConvertDBToPlayerData();
            NetworkSend.Send_LoginConfirmed(fromClientId, Helpers.SerializeObject(playerData));
        }
    }
}