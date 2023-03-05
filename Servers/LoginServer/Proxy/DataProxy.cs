using CerberusLoginServer.Networking;
using CommonData.DTOs;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using NovaCoreNetworking.Utils;
using System.Text.Json;

namespace LoginServer.Proxy {

    public static class DataProxy {
        public static List<BaseWeapon> _baseWeaponsData = new();
        public static Dictionary<int, BaseCharacter> _characterData = new();
        public static Dictionary<int, ErrorMessage> _errorMessages = new();

        public static async Task<List<BaseWeapon>> GetBaseWeapons() {
            if (_baseWeaponsData.Count == 0) {
                string baseWeaponString = await HttpRequests.GetBaseWeaponsAsync();

                var options = new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                };

                var weaponData = JsonSerializer.Deserialize<BaseWeaponsDTO>(baseWeaponString, options);

                if (weaponData != null)
                    _baseWeaponsData = weaponData.BaseWeapons;
                else throw new InvalidDataException("Error occured while getting base weapon data");
                NovaCoreLogger.Log(LogType.Info, "Got update base weapon data");
            }

            return _baseWeaponsData;
        }

        public static async Task<Dictionary<int, BaseCharacter>> GetCharacterData() {
            if (_characterData.Count == 0) {
                _characterData = await HttpRequests.GetCharacterDataAsync();
                NovaCoreLogger.Log(LogType.Info, "Got character data");
            }
            return _characterData;
        }

        public static async Task GetErrorMessagesAsync() {
            if (_errorMessages.Count == 0) {
                _errorMessages = await HttpRequests.GetErrorMessages();
                NovaCoreLogger.Log(LogType.Info, "Got error message data");
            }
        }
    }
}