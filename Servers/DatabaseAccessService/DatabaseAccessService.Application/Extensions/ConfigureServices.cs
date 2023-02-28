using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DatabaseAccessService.Application.Extensions {

    public static class ConfigureServices {

        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection) {
            //Add mediator
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            return serviceCollection;
        }
    }
}