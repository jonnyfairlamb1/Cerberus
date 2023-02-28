using CommonData.ServerData;

namespace CerberusLoginServer.Networking;

public static class NetworkManager {
    public static Dictionary<ushort, DBPlayer> _clientList = new();
    public static Dictionary<ushort, string> _errorMessages = new();
}