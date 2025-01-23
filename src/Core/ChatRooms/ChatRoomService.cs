using ChatService.Core.ChatRooms.Commands;
using ChatService.Core.ChatRooms.Models;
using Microsoft.Extensions.Logging;

namespace ChatService.Core.ChatRooms;

public class ChatRoomService : IChatRoomService
{
    private readonly ILogger<ChatRoomService> _logger;
    private readonly IChatroomRepository _chatRoomRepository;

    //todo later - inject user service
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

        try
        {
            var chatroom = await _chatRoomRepository.Get(id);

            if (chatroom is not null)
                _logger.LogInformation("Chatroom found");

            return chatroom;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get chatroom by id");
            throw;
        }
    }

    public async Task<Chatroom> Create(CreateChatRoomCommand command)
    {
        _logger.LogInformation("Creating chatroom");

        try
        {
            var newChatRoom = NewChatroom.Create(command.Username);
            var chatroom = await _chatRoomRepository.Create(newChatRoom);
            _logger.LogInformation("Chatroom created");
            return chatroom;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not create chatroom");
            throw;
        }
    }

    public async Task Update(UpdateChatRoomCommand command)
    {
        _logger.LogInformation("Updating chatroom");

        try
        {
            var chatRoom = await _chatRoomRepository.Get(command.ChatRoomId) ??
            throw new Exception("Chatroom not found");

            //todo - perform updates

            await _chatRoomRepository.Update(chatRoom);
            _logger.LogInformation("Chatroom updated");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not update chatroom");
            throw;
        }
    }

    public async Task Delete(DeleteChatRoomCommand command)
    {
        _logger.LogInformation("Deleting chatroom");

        try
        {
            var chatRoom = await _chatRoomRepository.Get(command.ChatRoomId) ??
            throw new Exception("Chatroom not found");

            await _chatRoomRepository.Delete(command.ChatRoomId);
            _logger.LogInformation("Chatroom deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not delete chatroom");
            throw;
        }
    }
}