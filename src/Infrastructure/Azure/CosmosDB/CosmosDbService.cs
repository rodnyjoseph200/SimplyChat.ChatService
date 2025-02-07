using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace ChatService.Infrastructure.Azure.CosmosDB;

[Service]
public class CosmosDbService : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient;
    private readonly Database _database;
    private readonly string _containerId;

    public CosmosDbService(CosmosClient cosmosClient, string databaseId, string containerId)
    {
        _cosmosClient = cosmosClient;
        _database = _cosmosClient.GetDatabase(databaseId);
        _containerId = containerId;
    }

    public Database GetDatabase => _database;

    public CosmosClient CosmosClient => _cosmosClient;

    public Container GetContainer => _database.GetContainer(_containerId);
}