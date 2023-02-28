using CommonData.PlayerSendData;
using CommonData.ServerData;
using System.Text.Json;

namespace CerberusLoginServer.Networking;

public static class HttpRequests {
    private static readonly HttpClient client = new HttpClient();

    public static async Task<DBPlayer> PlayerLoginAsync(string steamName, string steamID, string ipAddress) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        string request = "http://localhost:5000/Login?SteamName=" + steamName + "&SteamID=" + steamID + "&IPAddress=" + ipAddress;
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var player = await JsonSerializer.DeserializeAsync<DBPlayer>(await streamTask, options);
        return player!;
    }

    public static async Task<string> GetBaseWeaponsAsync() {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = "http://localhost:5000/BaseWeapons";
        Console.WriteLine(request);
        var weapons = await client.GetStringAsync(request);
        return weapons;
    }

    public static async Task<string> JoinRandomGame(string steamID) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        string request = "http://localhost:5000/JoinRandomGame?SteamID=" + steamID;
        Console.WriteLine(request);
        var gameServer = await client.GetStringAsync(request);
        return gameServer;
    }

    public static async Task<Dictionary<int, ErrorMessage>> GetErrorMessages() {
        Dictionary<int, ErrorMessage> errorMessages = new();
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = "http://localhost:5000/GetErrorMessages";
        Console.WriteLine(request);

        var streamTask = client.GetStreamAsync(request);
        errorMessages = await JsonSerializer.DeserializeAsync<Dictionary<int, ErrorMessage>>(await streamTask, options);

        return errorMessages;
    }

    public static async Task<Dictionary<int, BaseCharacter>> GetCharacterDataAsync() {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = "http://localhost:5000/CharacterData?";
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var characterData = await JsonSerializer.DeserializeAsync<Dictionary<int, BaseCharacter>>(await streamTask, options);
        return characterData;
    }
}