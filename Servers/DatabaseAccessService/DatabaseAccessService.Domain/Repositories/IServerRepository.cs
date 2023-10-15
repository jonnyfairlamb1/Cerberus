using CommonData.PlayerSendData;
using CommonData.ServerData;

namespace DatabaseAccessService.Domain.Repositories {

    public interface IServerRepository {

        Task<Dictionary<int, ErrorMessage>> GetErrorMessages();

        Task<Dictionary<int, GameMode>> GetAllGameModesAsync();

        Task<Dictionary<int, GameMaps>> GetAllMapsAsync();
    }
}