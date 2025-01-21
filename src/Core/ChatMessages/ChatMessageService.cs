using ChatService.Core.ChatMessages.Commands;
using ChatService.Core.ChatMessages.Models;
using ChatService.Core.ChatRooms;
using Microsoft.Extensions.Logging;

namespace ChatService.Core.ChatMessages;

public class ChatMessageService : IChatMessageService
{
    private readonly ILogger _logger;
    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatMessageService(ILogger<ChatRoomService> logger, IChatMessageRepository chatMessageRepository)
    {
        _logger = logger;
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<ChatMessage?> Get(string chatMessagId)
    {
        _logger.LogInformation("Getting chat message by chatMessageId");

        try
        {
            var chatMessage = await _chatMessageRepository.Get(chatMessagId);
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

    public async Task<IReadOnlyCollection<ChatMessage>> GetByChatRoomId(string chatroomId)
    {
        _logger.LogInformation("Getting chat messages by chatroomId");

        try
        {
            var chatMessages = await _chatMessageRepository.GetByChatRoomId(chatroomId);
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
            var newChatMessage = NewChatMessage.Create(command.ChatRoomId, command.UserId, command.Content, command.CreatedAt, command.Type);
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
            var chatMessage = await _chatMessageRepository.Get(command.ChatMessageId);
            if (chatMessage is null)
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
            var chatMessage = await _chatMessageRepository.Get(command.ChatMessageId);
            if (chatMessage is null)
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
