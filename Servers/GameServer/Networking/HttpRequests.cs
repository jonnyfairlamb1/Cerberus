using CommonData.DTOs;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using System.Text.Json;

namespace CerberusGameServer.Networking;

public static class HttpRequests {
    private static readonly HttpClient client = new HttpClient();

    public static string databaseServiceIpAddress = "localhost";
    public static int databaseServicePortNumber = 5000;

    public static async Task<Dictionary<int, ErrorMessage>> GetErrorMessages() {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/api/server/GetErrorMessages";
        Console.WriteLine(request);

        var streamTask = client.GetStreamAsync(request);
        var errorMessages = await JsonSerializer.DeserializeAsync<ErrorMessagesDTO>(await streamTask, options);

        return errorMessages.ErrorMessages;
    }

    public static async Task<GameServerData> RegisterServerAsync(string ipAddress, int port, int numberOfLobbies) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/api/server/RegisterServer?IPAddress="
            + ipAddress + "&Port=" + port + "&NumberOfLobbies=" + numberOfLobbies;

        Console.WriteLine(request);

        var streamTask = client.GetStreamAsync(request);
        var gameServerDto = await JsonSerializer.DeserializeAsync<GameServerDataDTO>(await streamTask, options);

        GameServerData gameServerData = new(gameServerDto);

        return gameServerData;
    }

    public static async Task<bool> CloseGameServer(int serverId) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/CloseGameServer?GameServerId=" + serverId;
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var successful = await JsonSerializer.DeserializeAsync<bool>(await streamTask, options);

        return successful!;
    }

    public static async Task<DBPlayer> GetPlayerData(string steamId) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/api/server/GetPlayerData?SteamId=" + steamId;
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var playerData = await JsonSerializer.DeserializeAsync<PlayerDataDTO>(await streamTask, options);

        return playerData.Player;
    }

    public static async Task<Dictionary<int, BaseCharacter>> GetCharacterData() {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/api/server/GetCharacterData";
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var characterData = await JsonSerializer.DeserializeAsync<CharacterDataDTO>(await streamTask, options);
        return characterData.characterData;
    }
}