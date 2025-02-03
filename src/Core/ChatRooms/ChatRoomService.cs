using ChatService.Core.ChatRooms.Commands;
using ChatService.Core.ChatRooms.Models;
using ChatService.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatService.Core.ChatRooms;

[Service]
public class ChatRoomService : IChatRoomService
{
    private readonly ILogger _logger;
    private readonly IChatroomRepository _chatRoomRepository;

    public ChatRoomService(ILogger<ChatRoomService> logger, IChatroomRepository chatRoomRepository)
    {
        _logger = logger;
        _chatRoomRepository = chatRoomRepository;
    }

    public async Task<Chatroom?> Get(string id)
    {
        _logger.LogInformation("Getting chatroom by id");

        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException($"{nameof(id)} is required");

        var chatroom = await _chatRoomRepository.Get(id);

        if (chatroom is not null)
            _logger.LogInformation("Chatroom found");
        else
            _logger.LogInformation("Chatroom not found");

        return chatroom;
    }

    public async Task<Chatroom> Create(CreateChatRoomCommand command)
    {
        _logger.LogInformation("Creating chatroom");

        var user = ChatRoomUser.CreateSuperUser(command.Username);

        var newChatRoom = NewChatroom.Create(user);
        var chatroom = await _chatRoomRepository.Create(newChatRoom);
        _logger.LogInformation("Chatroom created");
        return chatroom;
    }

    public async Task Update(UpdateChatRoomCommand command)
    {
        _logger.LogInformation("Updating chatroom");

        var chatRoom = await _chatRoomRepository.Get(command.ChatRoomId) ??
        throw new ResourceNotFoundException(nameof(Chatroom));

        //todo - perform updates

        await _chatRoomRepository.Update(chatRoom);
        _logger.LogInformation("Chatroom updated");
    }

    public async Task Delete(DeleteChatRoomCommand command)
    {
        _logger.LogInformation("Deleting chatroom");

        var chatRoom = await _chatRoomRepository.Get(command.ChatRoomId) ??
        throw new ResourceNotFoundException(nameof(Chatroom));

        await _chatRoomRepository.Delete(command.ChatRoomId);
        _logger.LogInformation("Chatroom deleted");
    }
}