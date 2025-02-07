using Microsoft.Azure.Cosmos;

namespace ChatService.Infrastructure.Azure.CosmosDB;

public interface ICosmosDbService
{
    Database GetDatabase { get; }

    CosmosClient CosmosClient { get; }

    Container GetContainer { get; }
}