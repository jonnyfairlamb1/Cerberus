using CommonData.ServerData;
using DatabaseAccessService.Domain.Repositories;
using DatabaseAccessService.Domain.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DatabaseAccessService.Domain.Services {

    public class PlayerAccountsService : IPlayerAccountsService {
        private readonly IMemoryCache _memoryCache;
        private readonly IPlayerAccountsRepository _playerAccountsRepository;

        private readonly int _playerTimeoutMins = 90;

        public PlayerAccountsService(IMemoryCache memoryCache, IPlayerAccountsRepository playerAccountsRepository) {
            _memoryCache = memoryCache;
            _playerAccountsRepository = playerAccountsRepository;
        }

        /// <inheritdoc/>
        public async Task<DBPlayer?> PlayerLoginAsync(string steamName, string steamID, string iPAddress) {
            bool hasValue = _memoryCache.TryGetValue(steamID, out DBPlayer? player);

            if (hasValue && player != null) {
                return player;
            }

            player = await _playerAccountsRepository.LoginAsync(steamName, steamID, iPAddress);

            _memoryCache.Set(steamID, player, new MemoryCacheEntryOptions {
                SlidingExpiration = TimeSpan.FromMinutes(_playerTimeoutMins)
            });

            return player;
        }

        /// <inheritdoc/>
        public async Task<bool> PlayerLogoutAsync(string steamID) {
            bool hasValue = _memoryCache.TryGetValue(steamID, out DBPlayer? player);

            if (hasValue && player != null) {
                bool loggedOut = await _playerAccountsRepository.LogoutAsync(steamID);
                _memoryCache.Remove(steamID);
                return loggedOut;
            }
            throw new InvalidDataException("Player requested logout and data was not found in cache");
        }

        /// <inheritdoc/>
        public async Task<DBPlayer> GetPlayerAsync(string steamID) {
            bool hasValue = _memoryCache.TryGetValue(steamID, out DBPlayer? player);

            if (hasValue && player != null) {
                return player;
            }

            player = await _playerAccountsRepository.GetPlayerDataAsync(steamID);

            _memoryCache.Set(steamID, player, new MemoryCacheEntryOptions {
                SlidingExpiration = TimeSpan.FromMinutes(_playerTimeoutMins)
            });
            return player;
        }
    }
}