using System.Reflection;

namespace DatabaseAccessService.Extensions {

    public static class ConfigureServices {

        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection) {
            //Add mediator
            serviceCollection.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return serviceCollection;
        }
    }
}