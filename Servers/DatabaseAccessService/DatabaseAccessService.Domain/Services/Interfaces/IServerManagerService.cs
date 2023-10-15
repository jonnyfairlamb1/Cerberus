using CommonData.PlayerSendData;
using CommonData.ServerData;
using DatabaseAccessService.Domain.Exceptions;

namespace DatabaseAccessService.Domain.Services.Interfaces {

    public interface IServerManagerService {

        /// <summary>
        /// Registers a game server with the service so that clients are able to connect to it. This
        /// should only be called from a game server.
        /// </summary>
        /// <param name="IP">Game servers IP Address</param>
        /// <param name="port">Game servers port number</param>
        /// <param name="amountOfLobbies">Amount of lobbies to create for that game server</param>
        /// <returns></returns>
        Task<GameServerData?> RegisterGameServerAsync(string IP, int port, int amountOfLobbies);

        /// <summary>
        /// Deregisters a game server from the service.
        /// </summary>
        /// <param name="gameServerId"></param>
        /// <returns></returns>
        Task<bool> CloseGameServerAsync(int gameServerId);

        /// <summary>
        /// Gets all of the error messages from the database or cache
        /// </summary>
        /// <returns></returns>
        /// <exception cref="RecordNotFoundException"></exception>
        Task<Dictionary<int, ErrorMessage>> GetErrorMessages();
    }
}