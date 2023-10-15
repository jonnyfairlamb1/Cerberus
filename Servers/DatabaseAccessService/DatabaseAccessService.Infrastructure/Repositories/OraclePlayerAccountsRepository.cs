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

        public Task<DBPlayer?> GetPlayerDataAsync(string steamID) {
            throw new NotImplementedException();
        }
    }
}