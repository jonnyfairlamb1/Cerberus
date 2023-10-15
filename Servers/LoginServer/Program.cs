using CerberusLoginServer.Networking;
using NovaCore.Utils;

namespace CerberusLoginServer {

    internal class Program {
        private static readonly ushort portNumber = 5100;
        private static readonly ushort maxPlayers = 10000;

        private static async Task Main() {
            Console.Title = "Cerberus Login Server";

            NovaCoreLogger.Initialize(Console.WriteLine, true);
            await NetworkConfig.InitializeServerAsync(portNumber, maxPlayers);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(NetworkConfig.CloseServer);
        }
    }
}