using CommonData.ServerData;

namespace DatabaseAccessService.Domain.Services.Interfaces {

    public interface IPlayerAccountsService {

        /// <summary>
        /// Log the player in, if they dont have account database will create one. Caches the player
        /// until they logout
        /// </summary>
        /// <param name="steamName"></param>
        /// <param name="steamID"></param>
        /// <param name="iPAddress"></param>
        /// <returns></returns>
        Task<DBPlayer?> PlayerLoginAsync(string steamName, string steamID, string iPAddress);

        /// <summary>
        /// Logs the player out and removes from the cache
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns></returns>
        Task<bool> PlayerLogoutAsync(string steamID);

        /// <summary>
        /// Gets the specified player by steamId
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns>DB player object, this holds all account data</returns>
        /// <exception cref="InvalidDataException"></exception>
        Task<DBPlayer> GetPlayerAsync(string steamID);
    }
}