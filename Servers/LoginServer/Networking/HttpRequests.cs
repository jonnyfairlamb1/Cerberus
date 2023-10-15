using CommonData.DTOs;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using System.Text.Json;

namespace CerberusLoginServer.Networking;

public static class HttpRequests {
    private static readonly HttpClient client = new HttpClient();

    //TODO: Read these from an app settings file.
    private static string _databaseServiceIpAddress = "localhost";
    private static int _databaseServicePortNumber = 5000;
    private const string _databaseServiceProtol = "http";

    private static readonly string _databaseServiceConnectionString;

    static HttpRequests()
    {
        _databaseServiceConnectionString = $"{_databaseServiceProtol}://{_databaseServiceIpAddress}:{_databaseServicePortNumber}/api";
    }


    public static async Task<DBPlayer?> PlayerLoginAsync(string steamName, string steamID, string ipAddress) {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
        string request = $"{_databaseServiceConnectionString}/Accounts/PlayerLogin?SteamName=" + steamName + "&SteamID=" + steamID + "&IPAddress=" + ipAddress;
        Console.WriteLine(request);
        var streamTask = client.GetStreamAsync(request);
        var player = await JsonSerializer.DeserializeAsync<PlayerLoginDTO>(await streamTask, options);

        if (player != null) return player.Player;
        else return null;
    }

    public static async Task<Dictionary<int, ErrorMessage>?> GetErrorMessages() {
        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        string request = $"{_databaseServiceConnectionString}/Server/GetErrorMessages";
        Console.WriteLine(request);

        var streamTask = client.GetStreamAsync(request);
        var messages = await JsonSerializer.DeserializeAsync<ErrorMessagesDTO>(await streamTask, options);

        if (messages != null)
            return messages.ErrorMessages;
        else
            return null;
    }
}