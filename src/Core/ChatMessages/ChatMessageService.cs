using ChatService.Core.ChatMessages.Commands;
using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Chatrooms;
using Microsoft.Extensions.Logging;

namespace ChatService.Core.ChatMessages;

public class ChatMessageService : IChatMessageService
{
    private readonly ILogger _logger;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IChatroomService _chatroomService;

    public ChatMessageService(ILogger<ChatroomService> logger, IChatMessageRepository chatMessageRepository, IChatroomService chatroomService)
    {
        _logger = logger;
        _chatMessageRepository = chatMessageRepository;
        _chatroomService = chatroomService;
    }

    public async Task<ChatMessage?> Get(string id)
    {
        _logger.LogInformation("Getting chat message by chatMessageId");

        try
        {
            var chatMessage = await _chatMessageRepository.Get(id);
            if (chatMessage is not null)
                _logger.LogInformation("Chat message found");

            return chatMessage;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get chat message");
            throw;
        }
    }

    public async Task<IReadOnlyCollection<ChatMessage>> GetByChatroomId(string chatroomId)
    {
        _logger.LogInformation("Getting chat messages by chatroomId");

        try
        {
            //todo use chatroomService to get chatroom by id
            //if not exists, throw exception. Else, get chat messages
            var chatMessages = await _chatMessageRepository.GetByChatroomId(chatroomId);
            if (chatMessages.Count is not 0)
                _logger.LogInformation("Chat messages found");

            return chatMessages;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get chat messages");
            throw;
        }
    }

    public Task<ChatMessage> Create(CreateChatMessageCommand command)
    {
        _logger.LogInformation("Creating chat message");

        try
        {
            //todo use chatroomService to get chatroom by id
            //if not exists, throw exception. Else, create chat message
            var newChatMessage = NewChatMessage.Create(command.ChatroomId, command.UserId, command.Content, command.CreatedAt, command.Type);
            var chatMessage = _chatMessageRepository.Create(newChatMessage);
            _logger.LogInformation("Chat message created");
            return chatMessage;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not create chat message");
            throw;
        }
    }

    public async Task Update(UpdateChatMessageCommand command)
    {
        _logger.LogInformation("Updating chat message");

        try
        {
            var chatMessage = await _chatMessageRepository.Get(command.ChatMessageId) ??
                throw new Exception("Chat message not found");

            chatMessage.UpdateContent(command.Content);

            await _chatMessageRepository.Update(chatMessage);
            _logger.LogInformation("Chat message updated");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not update chat message");
            throw;
        }
    }

    public async Task Delete(DeleteChatMessageCommand command)
    {
        _logger.LogInformation("Deleting chat message");

        try
        {
            var chatMessage = await _chatMessageRepository.Get(command.ChatMessageId) ??
                throw new Exception("Chat message not found");

            await _chatMessageRepository.Delete(command.ChatMessageId);
            _logger.LogInformation("Chat message deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not delete chat message");
            throw;
        }
    }

}