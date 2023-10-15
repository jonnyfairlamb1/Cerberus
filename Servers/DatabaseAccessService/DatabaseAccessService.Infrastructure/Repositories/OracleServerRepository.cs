using Common.Configuration;
using CommonData.Entities;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using DatabaseAccessService.Domain.Repositories;
using DatabaseAccessService.Infrastructure.Extensions;
using DatabaseAccessService.Infrastructure.Repositories;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseAccessService.Infrastructure.Repositorys;

public sealed class OracleServerRepository : OracleRepository, IServerRepository {

    public OracleServerRepository(IDbConfigurationContext<OracleConnection> dbConfigurationContext) : base(dbConfigurationContext) {
    }

    public async Task<Dictionary<int, ErrorMessage>> GetErrorMessages() {
        using OracleConnection connection = await GetOpenConnectionAsync();

        OracleDataReader reader = await connection.GetDBReader("PG_ERROR_HANDLING.GetErrorMessages");

        Dictionary<int, ErrorMessage> errorMessages = new();

        while (await reader.ReadAsync().ConfigureAwait(false)) {
            int errorId = await reader.SafeGetInt("ERROR_ID");
            string playerErrorMessage = await reader.SafeGetString("PLAYER_ERROR_MESSAGE");
            string internalErrorMessage = await reader.SafeGetString("INTERNAL_ERROR_MESSAGE");
            int errorLevel = await reader.SafeGetInt("ERROR_LEVEL");

            errorMessages.Add(errorId, new(errorId, playerErrorMessage, internalErrorMessage, errorLevel));
        }

        return errorMessages;
    }

    public async Task<Dictionary<int, GameMode>> GetAllGameModesAsync() {
        using OracleConnection connection = await GetOpenConnectionAsync();

        OracleDataReader reader = await connection.GetDBReader("PG_SERVER.GetGameModes");

        Dictionary<int, GameMode> gameModes = new();

        while (await reader.ReadAsync().ConfigureAwait(false)) {
            int modeId = await reader.SafeGetInt("GAME_MODE_ID");
            string abbreviation = await reader.SafeGetString("ABBREVIATION");
            string modeName = await reader.SafeGetString("GAME_MODE_NAME");
            string description = await reader.SafeGetString("DESCRIPTION");
            int amountOfTeams = await reader.SafeGetInt("AMOUNT_OF_TEAMS");
            int teamSize = await reader.SafeGetInt("TEAM_SIZE");
            int maxPlayers = await reader.SafeGetInt("MAX_PLAYERS");
            int maxSpectators = await reader.SafeGetInt("MAX_SPECTATORS");
            bool canJoinWithFriends = bool.Parse(await reader.SafeGetString("CAN_JOIN_WITH_FRIENDS"));

            gameModes.Add(modeId, new(modeId, abbreviation, modeName, description, amountOfTeams, teamSize, maxPlayers, maxSpectators, canJoinWithFriends));
        }

        return gameModes;
    }

    public async Task<Dictionary<int, GameMaps>> GetAllMapsAsync() {
        using OracleConnection connection = await GetOpenConnectionAsync();

        OracleDataReader reader = await connection.GetDBReader("PG_SERVER.GetMapDetails");

        Dictionary<int, GameMaps> maps = new();

        while (await reader.ReadAsync().ConfigureAwait(false)) {
            int mapId = await reader.SafeGetInt("MAP_ID");
            string mapName = await reader.SafeGetString("MAP_NAME");
            string sceneName = await reader.SafeGetString("SCENE_NAME");
            int gamemode = await reader.SafeGetInt("GAMEMODE");
            bool isActive = bool.Parse(await reader.SafeGetString("ISACTIVE"));

            maps.Add(mapId, new(mapName, sceneName, gamemode, isActive));
        }

        return maps;
    }
}