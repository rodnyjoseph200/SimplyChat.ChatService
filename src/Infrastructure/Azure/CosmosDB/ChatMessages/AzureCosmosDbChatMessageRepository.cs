using ChatService.Core.ChatMessages;
using ChatService.Core.ChatMessages.Models;
using Microsoft.Extensions.Logging;

namespace ChatService.Infrastructure.Azure.CosmosDB.ChatMessages;

public class AzureCosmosDbChatMessageRepository : IChatMessageRepository
{
    private readonly ILogger<AzureCosmosDbChatMessageRepository> _logger;

    public AzureCosmosDbChatMessageRepository(ILogger<AzureCosmosDbChatMessageRepository> logger)
    {
        _logger = logger;
    }

    public async Task<ChatMessage?> Get(string id)
    {
        _logger.LogInformation("Getting chat message");
        await Task.CompletedTask;
        _logger.LogInformation("Chat message found");
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<ChatMessage>> GetByChatRoomId(string chatroomId)
    {
        _logger.LogInformation("Getting chat messages by chatroomId");
        await Task.CompletedTask;
        _logger.LogInformation("Chat messages found");
        throw new NotImplementedException();
    }

    public async Task<ChatMessage> Create(NewChatMessage newChatMessage)
    {
        _logger.LogInformation("Creating chat message");
        await Task.CompletedTask;
        _logger.LogInformation("Chat message created");
        throw new NotImplementedException();
    }

    public async Task Update(ChatMessage chatMessage)
    {
        _logger.LogInformation("Updating chat message");
        await Task.CompletedTask;
        _logger.LogInformation("Chat message updated");
        throw new NotImplementedException();
    }

    public async Task Delete(string id)
    {
        _logger.LogInformation("Deleting chat message");
        await Task.CompletedTask;
        _logger.LogInformation("Chat message deleted");
        throw new NotImplementedException();
    }
}