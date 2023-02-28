using Oracle.ManagedDataAccess.Client;

namespace Common.Configuration {

    public class OracleContext : IDbConfigurationContext<OracleConnection> {

        #region Fields

        /// <summary>
        /// the database connection string
        /// </summary>
        private readonly string _connectionString;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise the connection information
        /// </summary>
        /// <param name="connectionString"></param>
        public OracleContext(string connectionString) {
            _connectionString = connectionString;
        }

        #endregion Constructors

        /// <summary>
        /// Get the connection string of the configuration provider
        /// </summary>
        /// <returns></returns>
        public OracleConnection CreateConnection() => new(_connectionString);
    }
}