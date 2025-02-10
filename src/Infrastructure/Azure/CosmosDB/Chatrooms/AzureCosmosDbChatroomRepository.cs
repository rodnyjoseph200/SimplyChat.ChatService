using System.Net;
using ChatService.Core.ChatRooms;
using ChatService.Core.ChatRooms.Models;
using ChatService.Infrastructure.Azure.CosmosDB.Chatrooms.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms;

[Service]
public class AzureCosmosDbChatroomRepository : IChatroomRepository
{
    private readonly ILogger<AzureCosmosDbChatroomRepository> _logger;
    private readonly Container _container;

    public AzureCosmosDbChatroomRepository(IAzureCosmosDbService provider, ILogger<AzureCosmosDbChatroomRepository> logger)
    {
        _container = provider.GetContainer;
        _logger = logger;
    }

    public async Task<Chatroom?> Get(string id)
    {
        _logger.LogInformation("Getting chatroom");

        try
        {
            var response = await _container.ReadItemAsync<AzureCosmosDbChatroom>(id, AzureCosmosDbChatroom.GetPartitionKey(id));
            if (response.Resource is null)
                throw new InvalidOperationException("resource is null");

            _logger.LogInformation("Chatroom found");
            return AzureCosmosDbChatroom.Convert(response.Resource);
        }
        catch (CosmosException ex) when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Item not found. Exception {Message}", ex.Message);
            return null;
        }
    }

    public async Task<Chatroom> Create(NewChatroom newChatroom)
    {
        _logger.LogInformation("Creating chatroom");

        var dbChatroom = AzureCosmosDbChatroom.Convert(newChatroom);
        try
        {
            var response = await _container.CreateItemAsync<AzureCosmosDbChatroom>(
                item: dbChatroom,
                partitionKey: AzureCosmosDbChatroom.GetPartitionKey(dbChatroom.Id));

            if (response.Resource is null)
                throw new InvalidOperationException("resource is null");

            _logger.LogInformation("Chatroom created. Request charge: {RequestCharge}", response.RequestCharge);

            return AzureCosmosDbChatroom.Convert(response.Resource);
        }
        catch (CosmosException ex) when (ex.StatusCode is HttpStatusCode.Conflict)
        {
            _logger.LogWarning("Chatroom Id already exists. Exception {Message}", ex.Message);
            throw;
        }
    }
    public async Task Update(Chatroom chatroom)
    {
        _logger.LogInformation("Updating chatroom");

        try
        {
            var response = await _container.ReplaceItemAsync(
                item: AzureCosmosDbChatroom.Update(chatroom),
                partitionKey: AzureCosmosDbChatroom.GetPartitionKey(chatroom),
                id: chatroom.Id);

            if (response.Resource is null)
                throw new InvalidOperationException("resource is null");

            _logger.LogInformation("Chatroom updated. Request charge: {RequestCharge}.", response.RequestCharge);
            return;
        }
        catch (CosmosException ex) when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Chatroom not found. Exception {exception}", ex.Message);
            throw;
        }
    }
    public async Task Delete(string chatroomId)
    {
        _logger.LogInformation("Deleting chatroom");

        try
        {
            var response = await _container.DeleteItemAsync<AzureCosmosDbChatroom>(
                id: chatroomId,
                partitionKey: AzureCosmosDbChatroom.GetPartitionKey(chatroomId));

            if (response.Resource is null)
                throw new InvalidOperationException("resource is null");

            _logger.LogInformation("Chatroom deleted. Request charge: {RequestCharge}.", response.RequestCharge);
            return;
        }
        catch (CosmosException ex) when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Chatroom not found. Exception {exception}", ex.Message);
            throw;
        }
    }
}