using CommonData.PlayerSendData;
using CommonData.ServerData;
using System.Text.Json;

namespace CerberusGameServer.Networking;

public static class HttpRequests {
    private static readonly HttpClient client = new HttpClient();

    public static string databaseServiceIpAddress = "localhost";
    public static int databaseServicePortNumber = 5000;

    public static async Task<Dictionary<int, ErrorMessage>> GetErrorMessages() {
        Dictionary<int, ErrorMessage> errorMessages = new();
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/GetErrorMessages";
        Console.WriteLine(request);

        var streamTask = client.GetStreamAsync(request);
        errorMessages = await JsonSerializer.DeserializeAsync<Dictionary<int, ErrorMessage>>(await streamTask, options);

        return errorMessages;
    }

    public static async Task<GameServerData> RegisterServerAsync(string ipAddress, int port, int numberOfLobbies) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/RegisterServer?IPAddress="
            + ipAddress + "&Port=" + port + "&NumberOfLobbies=" + numberOfLobbies;

        Console.WriteLine(request);

        var streamTask = client.GetStreamAsync(request);
        var gameServer = await JsonSerializer.DeserializeAsync<GameServerData>(await streamTask, options);
        return gameServer!;
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

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/GetPlayerData?SteamId=" + steamId;
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var playerData = await JsonSerializer.DeserializeAsync<DBPlayer>(await streamTask, options);

        return playerData!;
    }

    public static async Task<Dictionary<int, BaseCharacter>> GetCharacterData() {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/CharacterData?";
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var characterData = await JsonSerializer.DeserializeAsync<Dictionary<int, BaseCharacter>>(await streamTask, options);
        return characterData;
    }
}