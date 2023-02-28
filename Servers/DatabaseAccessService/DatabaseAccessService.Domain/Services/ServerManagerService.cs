using CommonData.PlayerSendData;
using CommonData.ServerData;
using DatabaseAccessService.Domain.Exceptions;
using DatabaseAccessService.Domain.Repositories;
using DatabaseAccessService.Domain.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DatabaseAccessService.Domain.Services {

    public class ServerManagerService : IServerManagerService {
        private readonly IMemoryCache _memoryCache;

        private readonly IServerRepository _serverRepository;

        private static Dictionary<int, GameServerData> _gameServers = new();
        private static int _gameServerId = 0;
        private readonly int _absoluteTimeoutMins = 10;

        public ServerManagerService(IMemoryCache memoryCache, IServerRepository serverRepository) {
            _memoryCache = memoryCache;
            _serverRepository = serverRepository;
        }

        /// <inheritdoc/>
        public async Task<GameServerData?> RegisterGameServerAsync(string IP, int port, int amountOfLobbies) {
            Dictionary<int, GameMaps>? gameMaps = await _memoryCache.GetOrCreateAsync("GameMaps", async entry => {
                entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(_absoluteTimeoutMins);
                return await _serverRepository.GetAllMapsAsync();
            });

            Dictionary<int, GameMode>? gameModes = await _memoryCache.GetOrCreateAsync("GameModes", async entry => {
                entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(_absoluteTimeoutMins);
                return await _serverRepository.GetAllGameModesAsync();
            });

            Random rnd = new();
            List<GameMaps> maps = new();
            List<GameMode> gamemodes = new();

            for (int i = 0; i < amountOfLobbies; i++) {
                int randomMapIndex = rnd.Next(0, gameMaps.Count - 1);

                if (!gameMaps.ElementAt(randomMapIndex).Value.IsActive) {
                    i--;
                    continue;
                }

                maps.Add(gameMaps.ElementAt(randomMapIndex).Value);
                gamemodes.Add(gameModes[maps[^1].GameMode]);
            }

            GameServerData gameServer = new(IP, port, _gameServerId, amountOfLobbies, maps, gamemodes);
            _gameServers.Add(_gameServerId, gameServer);
            _gameServerId++;
            return gameServer;
        }

        /// <inheritdoc/>
        public async Task<bool> CloseGameServerAsync(int gameServerId) {
            if (_gameServers.ContainsKey(gameServerId)) {
                _gameServers.Remove(gameServerId);
                return true;
            } else {
                Console.WriteLine("Attemped closing of server that does not exist");
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<Dictionary<int, ErrorMessage>> GetErrorMessages() {
            Dictionary<int, ErrorMessage>? errorMessages = await _memoryCache.GetOrCreateAsync("ErrorMessages", async entry => {
                return await _serverRepository.GetErrorMessages();
            });

            if (errorMessages == null)
                throw new RecordNotFoundException("No base character data was found.");
            return errorMessages;
        }

        /// <inheritdoc/>
        public async Task<Lobby?> JoinRandomGameAsync(DBPlayer joiningPlayer) {
            for (int i = 0; i < _gameServers.Count; i++) {
                for (int x = 0; x < _gameServers[i].Lobbies.Count; x++) {
                    if (_gameServers[i].Lobbies[x].Players.Count < _gameServers[i].Lobbies[i].MaxPlayers) {
                        _gameServers[i].Lobbies[x].Players.Add(joiningPlayer);
                        return _gameServers[i].Lobbies[x];
                    }
                }
            }
            return null;
        }

        /// <inheritdoc/>
        public async Task<List<BaseWeapon>> GetBaseWeaponsAsync() {
            List<BaseWeapon> baseWeapons = await _memoryCache.GetOrCreateAsync("BaseWeaponData", async entry => {
                entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(_absoluteTimeoutMins);
                return await _serverRepository.GetBaseWeaponsAsync();
            });

            if (baseWeapons.Count == 0)
                throw new RecordNotFoundException("The base weapon data could not be found");

            return baseWeapons;
        }

        /// <inheritdoc/>
        public async Task<Dictionary<int, BaseCharacter>> GetCharacterDataAsync() {
            Dictionary<int, BaseCharacter>? baseCharacters = await _memoryCache.GetOrCreateAsync("BaseCharacterData", async entry => {
                entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(_absoluteTimeoutMins);
                return await BuildBaseCharacterDataAsync();
            });

            if (baseCharacters == null)
                throw new RecordNotFoundException("No base character data was found.");
            return baseCharacters;
        }

        /// <summary>
        /// Builds the base character data as multiple database calls are needed
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<int, BaseCharacter>?> BuildBaseCharacterDataAsync() {
            Dictionary<int, BaseCharacter> characterData = new();
            characterData = await _serverRepository.GetCharacterDataAsync();

            for (int i = 0; i < characterData.Count; i++) {
                characterData.ElementAt(i).Value.characterAbilities = await _serverRepository.GetCharacterAbilitiesAsync(characterData.ElementAt(i).Value.CharacterId);
                characterData.ElementAt(i).Value.characterSkins = await _serverRepository.GetCharacterSkinsAsync(characterData.ElementAt(i).Value.CharacterId);
            }

            return characterData;
        }
    }
}