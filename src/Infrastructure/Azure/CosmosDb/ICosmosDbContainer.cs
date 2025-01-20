using Microsoft.Azure.Cosmos;

namespace ChatService.Infrastructure.Azure.CosmosDb;

public interface ICosmosDbContainer
{
    public Task<ItemResponse<T>> CreateItemAsync<T>(T item, PartitionKey? partitionKey = null, ItemRequestOptions? requestOptions = null, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<ItemResponse<T>> ReadItemAsync<T>(string id, PartitionKey partitionKey, ItemRequestOptions? requestOptions = null, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<ItemResponse<T>> ReplaceItemAsync<T>(T item, string id, PartitionKey? partitionKey = null, ItemRequestOptions? requestOptions = null, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<ItemResponse<T>> DeleteItemAsync<T>(string id, PartitionKey partitionKey, ItemRequestOptions? requestOptions = null, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
