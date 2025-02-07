using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace ChatService.Infrastructure.Azure.CosmosDB;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddAzureCosmosDb(this IServiceCollection services, string cosmosConnectionString, string databaseId, string containerId)
    {
        _ = services.AddServices();

        _ = services.AddSingleton(serviceProvider =>
        {
            return string.IsNullOrWhiteSpace(cosmosConnectionString)
                ? throw new InvalidOperationException("Cosmos DB connection string is missing")
                : new CosmosClient(cosmosConnectionString);
        });

        _ = services.AddSingleton<ICosmosDbService, CosmosDbService>(serviceProvider =>
        {
            var cosmosClient = serviceProvider.GetRequiredService<CosmosClient>();

            if (string.IsNullOrWhiteSpace(databaseId))
                throw new InvalidOperationException("Cosmos DB database ID is missing");

            if (string.IsNullOrWhiteSpace(containerId))
                throw new InvalidOperationException("Cosmos DB container ID is missing");

            return new CosmosDbService(cosmosClient, databaseId, containerId);
        });
        return services;
    }
}