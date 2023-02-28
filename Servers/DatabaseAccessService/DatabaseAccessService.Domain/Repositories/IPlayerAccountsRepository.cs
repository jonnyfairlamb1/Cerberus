using CommonData.Entities;
using CommonData.PlayerSendData;
using CommonData.ServerData;

namespace DatabaseAccessService.Domain.Repositories {

    public interface IPlayerAccountsRepository {

        Task<DBPlayer?> LoginAsync(string steamName, string steamID, string iPAddress);

        Task<DBPlayer?> GetPlayerDataAsync(string steamID);

        Task<bool> LogoutAsync(string steamID);

        Task<List<WeaponLoadouts>?> GetWeaponLoadoutsAsync(string steamID, List<CustomWeapon> customWeapons);

        Task<List<CustomWeapon>> GetCustomWeaponsAsync(string steamID);

        Task<List<OwnedSkinsTuple>> GetPlayerSkinsAsync(string steamID);

        Task<List<EquippedSkinsTuple>> GetPlayerEquippedSkinsAsync(string steamId);
    }
}