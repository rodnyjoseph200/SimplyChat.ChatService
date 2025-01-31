﻿using ChatService.Core.ChatMessages;
using ChatService.Core.ChatMessages.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Simply.Track;

namespace ChatService.Infrastructure.InMemoryDb.Testing.ChatMessages;

[Service]
public class InMemoryDbChatMessageRepository : IChatMessageRepository
{
    private readonly ILogger<InMemoryDbChatMessageRepository> _logger;

    public InMemoryDbChatMessageRepository(ILogger<InMemoryDbChatMessageRepository> logger)
    {
        _logger = logger;
    }

    public async Task<ChatMessage?> Get(string id)
    {
        _logger.LogInformation("Getting chat message");
        var chatMessage = InMemoryDbChatMessagesStore.InMemoryDbChatMessages.SingleOrDefault(x => x.Id == id);
        if (chatMessage is not null)
            _logger.LogInformation("Chat message found");

        await Task.CompletedTask;
        return chatMessage;
    }

    public async Task<IReadOnlyCollection<ChatMessage>> GetByChatRoomId(string chatroomId)
    {
        _logger.LogInformation("Getting chat messages by chatroomId");
        var chatMessages = InMemoryDbChatMessagesStore.InMemoryDbChatMessages.Where(x => x.ChatroomId == chatroomId)?.ToArray();

        if (chatMessages is not null && chatMessages.Length is not 0)
            _logger.LogInformation("Chat messages found");

        await Task.CompletedTask;
        return chatMessages ?? [];
    }

    public async Task<ChatMessage> Create(NewChatMessage newChatMessage)
    {
        _logger.LogInformation("Creating chat message");

        var chatMessage = ChatMessage.Load(
            Tracker.LoadTracking(DateTimeOffset.UtcNow, "test", DateTimeOffset.UtcNow, "test", false,
            null, null, null, null), Guid.NewGuid().ToString(), newChatMessage.ChatroomId,
            newChatMessage.UserId, newChatMessage.Content, newChatMessage.CreatedAt, newChatMessage.Type);

        InMemoryDbChatMessagesStore.Add(chatMessage);
        _logger.LogInformation("Chat message created");

        await Task.CompletedTask;
        return chatMessage;
    }

    public async Task Update(ChatMessage chatMessage)
    {
        _logger.LogInformation("Updating chat message");

        InMemoryDbChatMessagesStore.Remove(chatMessage.Id);
        InMemoryDbChatMessagesStore.Add(chatMessage);
        _logger.LogInformation("Chat message updated");

        await Task.CompletedTask;
    }

    public async Task Delete(string id)
    {
        _logger.LogInformation("Deleting chat message");
        InMemoryDbChatMessagesStore.Remove(id);
        _logger.LogInformation("Chat message deleted");

        await Task.CompletedTask;

    }
}