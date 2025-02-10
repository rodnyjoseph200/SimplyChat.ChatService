using Microsoft.Azure.Cosmos;

namespace ChatService.Infrastructure.Azure.CosmosDB;

public interface IAzureCosmosDbService
{
    Database GetDatabase { get; }

    CosmosClient CosmosClient { get; }

    Container GetContainer { get; }
}