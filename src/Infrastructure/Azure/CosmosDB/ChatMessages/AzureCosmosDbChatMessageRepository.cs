using System.Net;
using ChatService.Core.ChatMessages;
using ChatService.Core.ChatMessages.Models;
using ChatService.Infrastructure.Azure.CosmosDB.ChatMessages.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatService.Infrastructure.Azure.CosmosDB.ChatMessages;

[Service]
public class AzureCosmosDbChatMessageRepository : IChatMessageRepository
{
    private readonly ILogger<AzureCosmosDbChatMessageRepository> _logger;
    private readonly Container _container;

    public AzureCosmosDbChatMessageRepository(IAzureCosmosDbService provider, ILogger<AzureCosmosDbChatMessageRepository> logger)
    {
        _container = provider.GetContainer;
        _logger = logger;
    }

    public async Task<ChatMessage?> Get(string chatroomId, string chatMessageId)
    {
        _logger.LogInformation("Getting chat message");

        try
        {
            var response = await _container.ReadItemAsync<AzureCosmosDbChatMessage>(chatMessageId, AzureCosmosDbChatMessage.GetPartitionKey(chatroomId));
            if (response.Resource is null)
                throw new InvalidOperationException("resource is null");

            _logger.LogInformation("Chat message found");
            return AzureCosmosDbChatMessage.Convert(response.Resource);
        }
        catch (CosmosException ex) when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Item not found. Exception {Message}", ex.Message);
            return null;
        }
    }

    public async Task<IReadOnlyCollection<ChatMessage>> GetByChatRoomId(string chatroomId)
    {
        _logger.LogInformation("Getting chat messages by chatroomId");

        var queryable = _container.GetItemLinqQueryable<AzureCosmosDbChatMessage>(requestOptions: new QueryRequestOptions
        {
            PartitionKey = AzureCosmosDbChatMessage.GetPartitionKey(chatroomId),
            MaxItemCount = 50
        });

        using var iterator = queryable.ToFeedIterator();
        var items = new List<AzureCosmosDbChatMessage>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            items.AddRange(response);

            _logger.LogInformation("Retrieved a page of {Count} items (chat messages) with request charge {RequestCharge}.",
                response.Count, response.RequestCharge);
        }

        if (items.Count is not 0)
            _logger.LogInformation("Chat messages found");

        return AzureCosmosDbChatMessage.Convert(items);
    }

    public async Task<ChatMessage> Create(NewChatMessage newChatMessage)
    {
        _logger.LogInformation("Creating chat message");

        try
        {
            var response = await _container.CreateItemAsync<AzureCosmosDbChatMessage>(
                item: AzureCosmosDbChatMessage.Convert(newChatMessage),
                partitionKey: AzureCosmosDbChatMessage.GetPartitionKey(newChatMessage));

            if (response.Resource is null)
                throw new InvalidOperationException("resource is null");

            _logger.LogInformation("Chat message created. Request charge: {RequestCharge}", response.RequestCharge);

            return AzureCosmosDbChatMessage.Convert(response.Resource);
        }
        catch (CosmosException ex) when (ex.StatusCode is HttpStatusCode.Conflict)
        {
            _logger.LogWarning("ChatMessage Id already exists. Exception {Message}", ex.Message);
            throw;
        }
    }

    public async Task Update(ChatMessage chatMessage)
    {
        _logger.LogInformation("Updating chat message");

        try
        {
            var response = await _container.ReplaceItemAsync(
                item: AzureCosmosDbChatMessage.Update(chatMessage),
                partitionKey: AzureCosmosDbChatMessage.GetPartitionKey(chatMessage),
                id: chatMessage.Id);

            if (response.Resource is null)
                throw new InvalidOperationException("resource is null");

            _logger.LogInformation("Chat message updated. Request charge: {RequestCharge}.", response.RequestCharge);

            return;
        }
        catch (CosmosException ex) when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Chat message not found. Exception {exception}", ex.Message);
            throw;
        }
    }

    public async Task Delete(string chatroomId, string chatMessageId)
    {
        _logger.LogInformation("Deleting chat message");

        try
        {
            var response = await _container.DeleteItemAsync<AzureCosmosDbChatMessage>(
                id: chatMessageId,
                partitionKey: AzureCosmosDbChatMessage.GetPartitionKey(chatroomId)
            );

            if (response.Resource is null)
                throw new InvalidOperationException("resource is null");

            _logger.LogInformation("Chat message deleted. Request charge: {RequestCharge}.", response.RequestCharge);
            return;
        }
        catch (CosmosException ex) when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Chat message not found. Exception {exception}", ex.Message);
            throw;
        }
    }
}