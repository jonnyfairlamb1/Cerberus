using CommonData.PlayerSendData;
using CommonData.ServerData;

namespace DatabaseAccessService.Domain.Repositories {

    public interface IServerRepository {

        Task<Dictionary<int, ErrorMessage>> GetErrorMessages();

        Task<Dictionary<int, GameMode>> GetAllGameModesAsync();

        Task<Dictionary<int, GameMaps>> GetAllMapsAsync();

        Task<List<BaseWeapon>> GetBaseWeaponsAsync();

        Task<Dictionary<int, BaseCharacter>> GetCharacterDataAsync();

        Task<List<CharacterSkins>> GetCharacterSkinsAsync(int characterId);

        Task<List<BaseCharacterAbility>> GetCharacterAbilitiesAsync(int characterId);
    }
}