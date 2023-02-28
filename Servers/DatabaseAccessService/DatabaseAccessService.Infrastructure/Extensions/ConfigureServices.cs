using Common.Configuration;
using DatabaseAccessService.Domain.Repositories;
using DatabaseAccessService.Infrastructure.Repositories;
using DatabaseAccessService.Infrastructure.Repositorys;
using Microsoft.Extensions.DependencyInjection;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseAccessService.Infrastructure.Extensions {

    public static class ConfigureServices {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection) {
            string connectionString = "Data Source=localhost:1521/XEPDB1;User ID=CERBERUS;Password=letmein;Pooling=false;";

            var oracleContext = new OracleContext(connectionString);

            // Add database connection to db
            serviceCollection.AddSingleton<IDbConfigurationContext<OracleConnection>, OracleContext>(_ => oracleContext);

            // Add repositories
            serviceCollection.AddSingleton<IPlayerAccountsRepository, OraclePlayerAccountsRepository>();
            serviceCollection.AddSingleton<IServerRepository, OracleServerRepository>();

            //ConfigureCharacterFactories();

            return serviceCollection;
        }

        public static void ConfigureCharacterFactories() {
        }
    }
}