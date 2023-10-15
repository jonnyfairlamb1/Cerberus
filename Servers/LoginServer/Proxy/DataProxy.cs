using CerberusLoginServer.Networking;
using CommonData.DTOs;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using NovaCore.Utils;
using System.Text.Json;

namespace LoginServer.Proxy {

    public static class DataProxy {
        public static Dictionary<int, ErrorMessage> _errorMessages = new();

        public static async Task GetErrorMessagesAsync() {
            if (_errorMessages.Count == 0) {
                _errorMessages = await HttpRequests.GetErrorMessages();

                if (_errorMessages.Count == 0)
                {
                    NovaCoreLogger.Log(LogType.Error, "An error occured when getting error message data.");
                    return;
                }
                NovaCoreLogger.Log(LogType.Info, "Got error message data");
            }
        }
    }
}