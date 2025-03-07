using ChatService.Infrastructure.Azure.CosmosDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services
            .AddServices()
            .AddAzureCosmosDb(configuration);

        return services;
    }
}