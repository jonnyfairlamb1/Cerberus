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

    /// <summary>
    /// Gets all base weapons from database
    /// </summary>
    /// <returns></returns>
    public async Task<List<BaseWeapon>> GetBaseWeaponsAsync() {
        List<BaseWeapon> weapons = new();

        using OracleConnection connection = await GetOpenConnectionAsync();

        OracleDataReader reader = await connection.GetDBReader("PG_WEAPONS.GET_BASE_WEAPONS");

        while (await reader.ReadAsync().ConfigureAwait(false)) {
            int weaponId = await reader.SafeGetInt("WEAPON_ID");
            string loadoutSlot = await reader.SafeGetString("LOADOUT_SLOT");
            string weaponType = await reader.SafeGetString("WEAPON_TYPE");
            string displayName = await reader.SafeGetString("DISPLAY_NAME");
            string assetName = await reader.SafeGetString("ASSET_NAME");
            string shootStyle = await reader.SafeGetString("SHOOT_STYLE");

            float fireRate = await reader.SafeGetFloat("FIRE_RATE");
            int bulletRange = await reader.SafeGetInt("BULLET_RANGE");
            int bulletsPerFire = await reader.SafeGetInt("BULLETS_PER_FIRE");
            float bulletSpreadAmount = await reader.SafeGetFloat("BULLET_SPREAD_AMOUNT");
            float bulletAimSpreadAmount = await reader.SafeGetFloat("BULLET_AIM_SPREAD_AMOUNT");
            int xRecoilAmount = await reader.SafeGetInt("X_RECOIL_AMOUNT");
            int yRecoilAmount = await reader.SafeGetInt("Y_RECOIL_AMOUNT");
            float bulletPenAmount = await reader.SafeGetFloat("BULLET_PEN_AMOUNT");
            float bulletPenDamageReduction = await reader.SafeGetFloat("BULLET_PEN_DAMAGE_REDUCTION");
            int magazineSize = await reader.SafeGetInt("MAGAZINE_SIZE");
            float damagePerBullet = await reader.SafeGetFloat("DAMAGE_PER_BULLET");
            float reloadTime = await reader.SafeGetFloat("RELOAD_TIME");
            float wepWeightedMultiplier = await reader.SafeGetFloat("WEP_WEIGHT_MULTIPLIER");

            weapons.Add(new(weaponId, loadoutSlot, weaponType, displayName, assetName, shootStyle, fireRate, bulletRange, bulletsPerFire,
                            bulletSpreadAmount, bulletAimSpreadAmount, xRecoilAmount, yRecoilAmount, bulletPenAmount, bulletPenDamageReduction, magazineSize,
                            damagePerBullet, reloadTime, wepWeightedMultiplier));
        }
        return weapons;
    }

    public async Task<Dictionary<int, BaseCharacter>> GetCharacterDataAsync() {
        using OracleConnection connection = await GetOpenConnectionAsync();

        OracleDataReader reader = await connection.GetDBReader("PG_CHARACTER_DATA.GetCharacterData");

        Dictionary<int, BaseCharacter> characterData = new();

        while (await reader.ReadAsync().ConfigureAwait(false)) {
            int characterId = await reader.SafeGetInt("CHARACTER_ID");
            string characterName = await reader.SafeGetString("CHARACTER_NAME");
            int health = await reader.SafeGetInt("HEALTH");
            int runSpeed = await reader.SafeGetInt("RUN_SPEED");
            float footstepVolume = await reader.SafeGetFloat("FOOTSTEP_VOLUME");

            characterData.Add(characterId, new(characterId, characterName, health, runSpeed, footstepVolume, (CharacterEnum)characterId));
        }

        return characterData;
    }

    public async Task<List<CharacterSkins>> GetCharacterSkinsAsync(int characterId) {
        using OracleConnection connection = await GetOpenConnectionAsync();

        List<OracleParameter> parameters = new() {
                new OracleParameter {Value = characterId, OracleDbType = OracleDbType.Int32}
        };

        OracleDataReader reader = await connection.GetDBReader("PG_CHARACTER_DATA.GetAllCharacterSkins", parameters);

        List<CharacterSkins> skins = new();

        while (await reader.ReadAsync().ConfigureAwait(false)) {
            int skinId = await reader.SafeGetInt("SKIN_ID");
            string characterName = await reader.SafeGetString("CHARACTER_NAME");
            string skinName = await reader.SafeGetString("SKIN_NAME");
            int cost = await reader.SafeGetInt("COST");

            skins.Add(new(skinId, characterId, characterName, skinName, cost));
        }

        return skins;
    }

    public async Task<List<BaseCharacterAbility>> GetCharacterAbilitiesAsync(int characterId) {
        using OracleConnection connection = await GetOpenConnectionAsync();

        List<OracleParameter> parameters = new() {
                new OracleParameter {Value = characterId, OracleDbType = OracleDbType.Int32}
        };

        OracleDataReader reader = await connection.GetDBReader("PG_CHARACTER_DATA.GetCharacterAbilityData", parameters);

        List<BaseCharacterAbility> abilities = new();
        while (await reader.ReadAsync().ConfigureAwait(false)) {
            int abilityId = await reader.SafeGetInt("ABILITY_ID");
            string abilityName = await reader.SafeGetString("ABILITY_NAME");
            int healing = await reader.SafeGetInt("HEALING");
            int damage = await reader.SafeGetInt("DAMAGE");
            int cooldown = await reader.SafeGetInt("COOLDOWN");
            string abiltyBind = await reader.SafeGetString("ABILITY_BIND");
            string abiltyType = await reader.SafeGetString("ABILITY_TYPE");
            int ultimateChargeModifier = await reader.SafeGetInt("ULTIMATE_CHARGE_MODIFIER");

            abilities.Add(new(abilityId, characterId, abilityName, healing, damage, cooldown, abiltyBind, abiltyType, ultimateChargeModifier));
        }

        return abilities;
    }
}