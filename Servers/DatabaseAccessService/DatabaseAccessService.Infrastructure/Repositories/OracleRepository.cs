using Common.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseAccessService.Infrastructure.Repositories {

    public abstract class OracleRepository {
        private readonly IDbConfigurationContext<OracleConnection> _dbConfigurationContext;

        protected OracleRepository(IDbConfigurationContext<OracleConnection> dbConfigurationContext) {
            _dbConfigurationContext = dbConfigurationContext;
        }

        protected async Task<OracleConnection> GetOpenConnectionAsync() {
            OracleConnection connection = _dbConfigurationContext.CreateConnection();
            await connection.OpenAsync().ConfigureAwait(false);
            return connection;
        }
    }
}