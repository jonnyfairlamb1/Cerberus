﻿using LoginServer.Proxy;
using NovaCoreNetworking;
using NovaCoreNetworking.Utils;

namespace CerberusLoginServer.Networking;

public static class NetworkConfig {
    public static Server server;
    private static bool isRunning;
    private static Thread serverLoopThread;

    private static bool _acceptingConnections = false;

    public static async Task InitializeServerAsync(ushort portNumber, int maxPlayers) {
        isRunning = true;

        Message.MaxPayloadSize = 8096;

        server = new Server {
            TimeoutTime = ushort.MaxValue // Max value timeout to avoid getting timed out for as long as possible when testing with very high loss rates (if all heartbeat messages are lost during this period of time, it will trigger a disconnection)
        };
        server.Start(portNumber, (ushort)maxPlayers);

        server.ClientConnected += Server_ClientConnected;

        serverLoopThread = new Thread(NetworkConfig.ServerLoop);
        serverLoopThread.Start();

        await DataProxy.GetErrorMessagesAsync();
        await DataProxy.GetBaseWeapons();
        await DataProxy.GetCharacterData();
        _acceptingConnections = true;
    }

    private static void Server_ClientConnected(object? sender, ServerConnectedEventArgs e) {
        if (!_acceptingConnections) {
            Message message = Message.Create(MessageSendMode.Reliable, (ushort)Packets.LoginServerPackets.LS_RejectedConnection_ServerNotReady);
            server.Reject(e.Client, message);
        }
    }

    public static void ServerLoop() {
        while (isRunning) {
            server.Update();
            Thread.Sleep(10);
        }

        server.Stop();
        NovaCoreLogger.Log(LogType.Info, "Closing Server");
    }

    public static void CloseServer(object sender, EventArgs e) {
        isRunning = false;
        serverLoopThread.Join();
    }

    public static string GetIPAddress(ushort connectionID) {
        for (int i = 0; i < server.Clients.Length; i++) {
            if (server.Clients[i].Id == connectionID) {
                Connection connection = server.Clients[i];
                string ipAddress = connection.ToString().Split(":").First();
                return ipAddress;
            }
        }
        return String.Empty;
    }
}