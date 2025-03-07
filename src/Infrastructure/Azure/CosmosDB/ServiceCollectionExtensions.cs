using ChatService.Infrastructure.Azure.CosmosDB.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatService.Infrastructure.Azure.CosmosDB;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureCosmosDb(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.Configure<CosmosDbClientOptions>(configuration.GetSection(CosmosDbClientOptions.Name));

        var cosmosDbClientOptions = configuration.GetSection(CosmosDbClientOptions.Name).Get<CosmosDbClientOptions>();
        cosmosDbClientOptions = CosmosDbClientOptions.Validate(cosmosDbClientOptions);

        _ = services
            .AddSingleton(new CosmosClient(cosmosDbClientOptions.ConnectionString))
            .AddSingleton<ICosmosDbService, CosmosDbService>(serviceProvider =>
            {
                var cosmosClient = serviceProvider.GetRequiredService<CosmosClient>();
                return new CosmosDbService(cosmosClient, cosmosDbClientOptions.DatabaseId, cosmosDbClientOptions.ContainerId);
            });

        return services;
    }
}