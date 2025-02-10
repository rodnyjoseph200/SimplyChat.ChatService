using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatService.Infrastructure.Azure.CosmosDB;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddAzureCosmosDb(this IServiceCollection services, IConfiguration configuration)
    {
        //todo
        //_ = services.Configure<AzureCosmosDbOptions>(configuration.GetSection(AzureCosmosDbOptions.Name));

        //_ = services.AddServices();

        //_ = services.AddSingleton(serviceProvider =>
        //{
        //    return string.IsNullOrWhiteSpace(cosmosConnectionString)
        //        ? throw new InvalidOperationException("Cosmos DB connection string is missing")
        //        : new CosmosClient(cosmosConnectionString);
        //});

        //_ = services.AddSingleton<IAzureCosmosDbService, AzureCosmosDbService>(serviceProvider =>
        //{
        //    var cosmosClient = serviceProvider.GetRequiredService<CosmosClient>();

        //    if (string.IsNullOrWhiteSpace(databaseId))
        //        throw new InvalidOperationException("Cosmos DB database ID is missing");

        //    if (string.IsNullOrWhiteSpace(containerId))
        //        throw new InvalidOperationException("Cosmos DB container ID is missing");

        //    return new AzureCosmosDbService(cosmosClient, databaseId, containerId);
        //});
        return services;
    }
}