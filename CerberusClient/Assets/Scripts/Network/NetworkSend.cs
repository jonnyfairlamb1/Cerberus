using CerberusClient.Network.Data;
using NovaCoreNetworking;
using Packets;

public static class NetworkSend {

    public static void SendLogin(string steamId, string steamName) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientPackets.C_Login);
        message.AddString(steamName);
        message.AddString(steamId);
        NetworkManager.instance.Client.Send(message);
    }

    public static void SendJoinGameRequest() {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientPackets.C_JoinRandomGame);
        message.AddString(GameManager.instance._localPlayerData.steamID);
        NetworkManager.instance.Client.Send(message);
    }

    public static void SendLobbyIdToGameServer(GameServerData gameServerData) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientPackets.C_LobbyId);
        message.AddInt(gameServerData.lobbyID);
        message.AddString(GameManager.instance._localPlayerData.steamID);
        NetworkManager.instance.Client.Send(message);
    }

    public static void SendChoseCharacterToGameServer(BaseCharacter character) {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientPackets.C_PlayerChoseCharacter);
        message.Add(GameManager.instance._localPlayerData.steamID);
        message.Add(character.CharacterId);
        NetworkManager.instance.Client.Send(message);
    }
}