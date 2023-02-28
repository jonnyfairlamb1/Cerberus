using GameServer.General;
using GameServer.Networking;
using NovaCoreNetworking;
using NovaCoreNetworking.Utils;

namespace CerberusGameServer.Networking;

public static class NetworkConfig {
    public static Server server;
    private static bool isRunning;
    private static Thread serverLoopThread;

    public static async Task InitializeServerAsync(ushort portNumber, ushort maxPlayers) {
        isRunning = true;

        Message.MaxPayloadSize = 8096;

        server = new Server {
            TimeoutTime = ushort.MaxValue // Max value timeout to avoid getting timed out for as long as possible when testing with very high loss rates (if all heartbeat messages are lost during this period of time, it will trigger a disconnection)
        };
        server.Start(portNumber, maxPlayers);

        server.ClientConnected += Server_ClientConnected;
        server.ClientDisconnected += Server_ClientDisconnected;
        serverLoopThread = new Thread(NetworkConfig.ServerLoop);
        serverLoopThread.Start();
    }

    private static void Server_ClientConnected(object? sender, ServerConnectedEventArgs e) {
        NetworkSend.SendClientId(e.Client.Id);
    }

    private static void Server_ClientDisconnected(object? sender, ServerDisconnectedEventArgs e) {
        //TODO: Find the lobby id from the client connection ID
    }

    public static void ServerLoop() {
        while (isRunning) {
            server.Update();
            Thread.Sleep(10);
        }

        bool successful = HttpRequests.CloseGameServer(ServerData._gameServer.GameServerId).Result;
        if (successful) {
            server.Stop();
            NovaCoreLogger.Log(LogType.Info, "Closing Server");
        } else {
            while (true) {
                NovaCoreLogger.Log(LogType.Info, "Waiting for server to be disconnected");
                successful = HttpRequests.CloseGameServer(ServerData._gameServer.GameServerId).Result;
                if (successful) {
                    server.Stop();
                    NovaCoreLogger.Log(LogType.Info, "Closing Server");
                    return;
                }
            }
        }
    }

    public static void CloseServer(object sender, EventArgs e) {
        isRunning = false;
        serverLoopThread.Join();
    }
}