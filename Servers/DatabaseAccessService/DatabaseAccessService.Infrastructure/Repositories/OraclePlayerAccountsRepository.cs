using Common.Configuration;
using CommonData;
using CommonData.Entities;
using CommonData.PlayerSendData;
using CommonData.ServerData;
using DatabaseAccessService.Domain.Repositories;
using DatabaseAccessService.Infrastructure.Extensions;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseAccessService.Infrastructure.Repositories {

    public sealed class OraclePlayerAccountsRepository : OracleRepository, IPlayerAccountsRepository {

        #region Constructors

        public OraclePlayerAccountsRepository(IDbConfigurationContext<OracleConnection> dbConfigurationContext) : base(dbConfigurationContext) {
        }

        #endregion Constructors

        /// <summary>
        /// Logs a player in, if they dont have an account the database creates one
        /// </summary>
        /// <param name="steamName"></param>
        /// <param name="steamID"></param>
        /// <param name="iPAddress"></param>
        /// <returns></returns>
        public async Task<DBPlayer?> LoginAsync(string steamName, string steamID, string iPAddress) {
            try {
                List<OracleParameter> parameters = new() {
                new OracleParameter {Value = steamID, OracleDbType = OracleDbType.Varchar2},
                new OracleParameter {Value = steamName, OracleDbType = OracleDbType.Varchar2},
                new OracleParameter {Value = iPAddress, OracleDbType = OracleDbType.Varchar2},
            };

                using OracleConnection connection = await GetOpenConnectionAsync();

                OracleDataReader reader = await connection.GetDBReader("PG_ACCOUNTS.Player_Login", parameters);

                DBPlayer? player = new();

                if (!reader.HasRows)
                    return null;
                await reader.ReadAsync();
                player.SteamName = steamName;
                player.SteamID = steamID;
                player.PlayerLevel = await reader.SafeGetInt("PLAYER_LEVEL");
                player.BattlepassLevel = await reader.SafeGetInt("BATTLEPASS_LEVEL");
                player.Currency = await reader.SafeGetInt("CURRENCY");
                player.IpAddress = iPAddress;
                player.AccountStanding = await reader.SafeGetString("ACCOUNT_STANDING");

                return player;
            } catch (Exception e) {
                ServerLogger.WriteError("Error: " + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Logs a player out of the game.
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns></returns>
        public async Task<bool> LogoutAsync(string steamID) {
            try {
                List<OracleParameter> parameters = new() {
                    new OracleParameter {Value = steamID, OracleDbType = OracleDbType.Varchar2},
                };

                using OracleConnection connection = await GetOpenConnectionAsync();

                OracleDataReader reader = await connection.GetDBReader("PG_ACCOUNTS.PLAYER_LOGOUT", parameters);

                int isLoggedIn = 0;
                while (await reader.ReadAsync().ConfigureAwait(false)) {
                    isLoggedIn = await reader.SafeGetInt("IS_LOGGED_IN");
                }
                if (isLoggedIn == 1) return true;
                else return false;
            } catch (Exception e) {
                ServerLogger.WriteError("Error: " + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Gets weapon loads for player and applies weapon objects created by getcustomweapons
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns></returns>
        public async Task<List<WeaponLoadouts>?> GetWeaponLoadoutsAsync(string steamID, List<CustomWeapon> customWeapons) {
            try {
                List<WeaponLoadouts> weaponLoadouts = new();

                List<OracleParameter> parameters = new() {
                    new OracleParameter {Value = steamID, OracleDbType = OracleDbType.Varchar2}
                };

                using OracleConnection connection = await GetOpenConnectionAsync();
                OracleDataReader reader = await connection.GetDBReader("PG_WEAPONS.GET_PLAYER_LOADOUTS", parameters);

                while (await reader.ReadAsync().ConfigureAwait(false)) {
                    int primaryWepId = await reader.SafeGetInt("CUSTOM_PRIMARY_ID");
                    int secondaryWepId = await reader.SafeGetInt("CUSTOM_SECONDARY_ID");

                    CustomWeapon? primaryWep = null;
                    CustomWeapon? secondaryWep = null;
                    for (int i = 0; i < customWeapons.Count; i++) {
                        if (primaryWep != null && secondaryWep != null) break;

                        if (customWeapons[i].CustomWeaponID == primaryWepId) {
                            primaryWep = customWeapons[i];
                        }
                        if (customWeapons[i].CustomWeaponID == secondaryWepId) {
                            secondaryWep = customWeapons[i];
                        }
                    }
                    int loadoutId = await reader.SafeGetInt("LOADOUT_ID");
                    string owningSteamId = await reader.SafeGetString("OWNING_PLAYER_STEAM_ID");
                    string loadoutName = await reader.SafeGetString("LOADOUT_NAME");

                    weaponLoadouts.Add(new(loadoutId, owningSteamId, loadoutName, primaryWepId, secondaryWepId, primaryWep, secondaryWep));
                }
                return weaponLoadouts;
            } catch (Exception e) {
                ServerLogger.WriteError("Error: " + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all custom weapons for that player
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns></returns>
        public async Task<List<CustomWeapon>> GetCustomWeaponsAsync(string steamID) {
            try {
                List<CustomWeapon> customWeapons = new();

                List<OracleParameter> parameters = new() {
                    new OracleParameter {Value = steamID, OracleDbType = OracleDbType.Varchar2}
                };

                using OracleConnection connection = await GetOpenConnectionAsync();
                OracleDataReader reader = await connection.GetDBReader("PG_WEAPONS.GET_CUSTOM_WEAPONS", parameters);

                while (await reader.ReadAsync().ConfigureAwait(false)) {
                    int customWepId = await reader.SafeGetInt("CUSTOM_WEAPON_ID");
                    string loadoutSlot = await reader.SafeGetString("LOADOUT_SLOT");
                    int weaponId = await reader.SafeGetInt("WEAPON_ID");
                    string owningPlayerSteamId = await reader.SafeGetString("OWNING_PLAYER_STEAM_ID");

                    int barrelID = await reader.SafeGetInt("BARREL_ATTACHMENT_ID");
                    int gripID = await reader.SafeGetInt("GRIP_ATTACHMENT_ID");
                    int scopeID = await reader.SafeGetInt("SCOPE_ATTACHMENT_ID");
                    int railID = await reader.SafeGetInt("RAIL_ATTACHMENT_ID");
                    int midBarrelID = await reader.SafeGetInt("MID_BARREL_ATTACHMENT_ID");

                    customWeapons.Add(new(customWepId, loadoutSlot, weaponId, owningPlayerSteamId, barrelID, scopeID, gripID, railID, midBarrelID));
                }
                return customWeapons;
            } catch (Exception e) {
                ServerLogger.WriteError("Error: " + e.ToString());
                return null;
            }
        }

        public async Task<List<OwnedSkinsTuple>> GetPlayerSkinsAsync(string steamID) {
            List<OwnedSkinsTuple> ownedSkin = new();

            List<OracleParameter> parameters = new() {
                new OracleParameter {Value = steamID, OracleDbType = OracleDbType.Varchar2}
            };

            using OracleConnection connection = await GetOpenConnectionAsync();

            OracleDataReader reader = await connection.GetDBReader("PG_ACCOUNTS.GetPlayerCharacterSkins", parameters);

            while (await reader.ReadAsync().ConfigureAwait(false)) {
                int skinId = await reader.SafeGetInt("SKIN_ID");
                int characterId = await reader.SafeGetInt("CHARACTER_ID");

                ownedSkin.Add(new(characterId, skinId));
            }
            return ownedSkin;
        }

        public async Task<List<EquippedSkinsTuple>> GetPlayerEquippedSkinsAsync(string steamId) {
            List<EquippedSkinsTuple> equippedSkins = new();

            List<OracleParameter> parameters = new() {
                new OracleParameter {Value = steamId, OracleDbType = OracleDbType.Varchar2}
        };

            using OracleConnection connection = await GetOpenConnectionAsync();
            OracleDataReader reader = await connection.GetDBReader("PG_ACCOUNTS.GetPlayerEquippedSkins", parameters);

            while (await reader.ReadAsync().ConfigureAwait(false)) {
                int skinId = await reader.SafeGetInt("SKIN_ID");
                int characterId = await reader.SafeGetInt("CHARACTER_ID");

                equippedSkins.Add(new(characterId, skinId));
            }

            return equippedSkins;
        }

        public Task<DBPlayer?> GetPlayerDataAsync(string steamID) {
            throw new NotImplementedException();
        }
    }
}