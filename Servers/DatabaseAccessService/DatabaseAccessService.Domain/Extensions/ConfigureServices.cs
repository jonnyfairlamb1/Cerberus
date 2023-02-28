using DatabaseAccessService.Domain.Services;
using DatabaseAccessService.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseAccessService.Domain.Extensions {

    public static class ConfigureServices {

        public static IServiceCollection AddDomainServices(this IServiceCollection serviceCollection) {
            serviceCollection.AddSingleton<IServerManagerService, ServerManagerService>();
            serviceCollection.AddSingleton<IPlayerAccountsService, PlayerAccountsService>();

            return serviceCollection;
        }
    }
}