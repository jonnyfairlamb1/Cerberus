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

        DBPlayer player = HttpRequests.PlayerLoginAsync(steamName, steamID, NetworkConfig.GetIPAddress(fromClientId)).Result;
        PlayerData playerData = player.ConvertDBToPlayerData();

        if (player == null) {
            NetworkSend.Send_ErrorMessage(fromClientId, DataProxy._errorMessages[10002]);
        } else {
            NetworkManager._clientList.Add(fromClientId, player);

            NetworkSend.Send_LoginConfirmed(fromClientId, Helpers.SerializeObject(playerData));

            NetworkSend.Send_AllCharacterData(fromClientId);
            NetworkSend.Send_PlayerOwnedSkins(fromClientId, player.OwnedPlayerSkins);
            NetworkSend.Send_PlayerEquippedSkins(fromClientId, player.EquippedSkins);

            NetworkSend.Send_BaseWeaponData(fromClientId);
            NetworkSend.Send_WeaponLoadouts(fromClientId, player.WeaponLoadouts);

            NetworkSend.Send_EndOfPlayerData(fromClientId);
        }
    }

    [MessageHandler((ushort)ClientPackets.C_JoinRandomGame)]
    private static void Packet_JoinRandomGame(ushort fromClientId, Message message) {
        NovaCoreLogger.Log(LogType.Debug, "Received Join Random Game Message");
        string steamID = message.GetString();

        string gameServer = HttpRequests.JoinRandomGame(steamID).Result;
        NetworkSend.Send_JoinedGame(fromClientId, gameServer);
    }
}