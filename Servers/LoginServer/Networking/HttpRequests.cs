using CommonData.DTOs;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using System.Text.Json;

namespace CerberusLoginServer.Networking;

public static class HttpRequests {
    private static readonly HttpClient client = new HttpClient();

    public static string databaseServiceIpAddress = "localhost";
    public static int databaseServicePortNumber = 5000;

    public static async Task<DBPlayer?> PlayerLoginAsync(string steamName, string steamID, string ipAddress) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/api/Accounts/Login?SteamName=" + steamName + "&SteamID=" + steamID + "&IPAddress=" + ipAddress;
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var player = await JsonSerializer.DeserializeAsync<PlayerLoginDTO>(await streamTask, options);

        if (player != null) return player.Player;
        else return null;
    }

    public static async Task<string> GetBaseWeaponsAsync() {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/api/Server/BaseWeapons";
        Console.WriteLine(request);
        var weapons = await client.GetStringAsync(request);
        return weapons;
    }

    public static async Task<string> JoinRandomGame(string steamID) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/api/Server/JoinRandomGame?SteamID=" + steamID;
        Console.WriteLine(request);
        var gameServer = await client.GetStringAsync(request);
        return gameServer;
    }

    public static async Task<Dictionary<int, ErrorMessage>?> GetErrorMessages() {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/api/Server/GetErrorMessages";
        Console.WriteLine(request);

        var streamTask = client.GetStreamAsync(request);
        var messages = await JsonSerializer.DeserializeAsync<ErrorMessagesDTO>(await streamTask, options);

        if (messages != null)
            return messages.ErrorMessages;
        else
            return null;
    }

    public static async Task<Dictionary<int, BaseCharacter>?> GetCharacterDataAsync() {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"http://{databaseServiceIpAddress}:{databaseServicePortNumber}/api/Server/GetCharacterData";
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var characterData = await JsonSerializer.DeserializeAsync<CharacterDataDTO>(await streamTask, options);
        return characterData.characterData;
    }
}