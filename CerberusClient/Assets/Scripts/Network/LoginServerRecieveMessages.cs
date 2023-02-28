using CerberusClient.Network.Data;
using NovaCoreNetworking;
using Packets;
using UnityEngine;

public static class LoginServerReceiveMessages {

    [MessageHandler((ushort)LoginServerPackets.LS_JoinedGame)]
    private static void Packet_JoinGameConfirmed(Message message) {
        string gameServerObject = message.GetString();
        GameServerData gameServerData = JsonUtility.FromJson<GameServerData>(gameServerObject);

        NetworkManager.instance.ConnectGameServer(gameServerData);
        MenuManager.instance.StartLoadingScreen(gameServerData.mapName, gameServerData.gameMode, gameServerData.abbreviation);
    }
}