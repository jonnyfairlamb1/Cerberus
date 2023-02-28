namespace Common.Configuration {

    public interface IDbConfigurationContext<out T> where T : class {

        /// <summary>
        /// Create the database connection string
        /// </summary>
        /// <returns></returns>
        T CreateConnection();
    }
}