using ChatService.Core.ChatMessages.Commands;
using ChatService.Core.ChatMessages.Models;
using ChatService.Core.ChatRooms;
using ChatService.Core.ChatRooms.Models;
using ChatService.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace ChatService.Core.ChatMessages;

public class ChatMessageService : IChatMessageService
{
    private readonly ILogger _logger;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IChatRoomService _chatRoomService;

    public ChatMessageService(ILogger<ChatRoomService> logger, IChatMessageRepository chatMessageRepository, IChatRoomService chatRoomService)
    {
        _logger = logger;
        _chatMessageRepository = chatMessageRepository;
        _chatRoomService = chatRoomService;
    }

    public async Task<ChatMessage?> Get(string id)
    {
        _logger.LogInformation("Getting chat message by chatMessageId");

        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException($"{nameof(id)} is required");

        var chatMessage = await _chatMessageRepository.Get(id);
        if (chatMessage is not null)
            _logger.LogInformation("Chat message found");

        return chatMessage;
    }

    public async Task<IReadOnlyCollection<ChatMessage>> GetByChatRoomId(string chatroomId)
    {
        _logger.LogInformation("Getting chat messages by chatroomId");

        if (string.IsNullOrWhiteSpace(chatroomId))
            throw new ArgumentException($"{nameof(chatroomId)} is required");

        var chatroom = await _chatRoomService.Get(chatroomId) ?? throw new BadRequestException($"{nameof(Chatroom)} not found");

        var chatMessages = await _chatMessageRepository.GetByChatRoomId(chatroom.Id);
        if (chatMessages.Count is not 0)
            _logger.LogInformation("Chat messages found");

        return chatMessages;
    }

    public async Task<ChatMessage> Create(CreateChatMessageCommand command)
    {
        _logger.LogInformation("Creating chat message");

        var chatroom = await _chatRoomService.Get(command.ChatroomId) ?? throw new BadRequestException($"{nameof(Chatroom)} not found");

        var newChatMessage = NewChatMessage.Create(command.ChatroomId, command.UserId, command.Content, command.CreatedAt, command.Type);
        var chatMessage = await _chatMessageRepository.Create(newChatMessage);
        _logger.LogInformation("Chat message created");
        return chatMessage;
    }

    public async Task Update(UpdateChatMessageCommand command)
    {
        _logger.LogInformation("Updating chat message");

        var chatMessage = await _chatMessageRepository.Get(command.ChatMessageId) ??
            throw new ResourceNotFoundException(nameof(ChatMessage));

        chatMessage.UpdateContent(command.Content);

        await _chatMessageRepository.Update(chatMessage);
        _logger.LogInformation("Chat message updated");
    }

    public async Task Delete(DeleteChatMessageCommand command)
    {
        _logger.LogInformation("Deleting chat message");

        var chatMessage = await _chatMessageRepository.Get(command.ChatMessageId) ??
            throw new ResourceNotFoundException(nameof(ChatMessage));

        await _chatMessageRepository.Delete(command.ChatMessageId);
        _logger.LogInformation("Chat message deleted");
    }
}