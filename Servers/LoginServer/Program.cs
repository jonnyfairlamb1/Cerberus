using CerberusLoginServer.Networking;
using NovaCoreNetworking.Utils;

namespace CerberusLoginServer {

    internal class Program {
        private static readonly ushort portNumber = 7777;
        private static readonly ushort maxPlayers = 10000;

        private static void Main() {
            Console.Title = "Cerberus Login Server";

            NovaCoreLogger.Initialize(Console.WriteLine, true);
            NetworkConfig.InitializeServerAsync(portNumber, maxPlayers);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(NetworkConfig.CloseServer);
        }
    }
}