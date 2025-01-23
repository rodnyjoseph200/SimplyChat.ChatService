using ChatService.Core.Chatrooms.Commands;
using ChatService.Core.Chatrooms.Models;
using Microsoft.Extensions.Logging;

namespace ChatService.Core.Chatrooms;

public class ChatroomService : IChatroomService
{
    private readonly ILogger<ChatroomService> _logger;
    private readonly IChatroomRepository _chatroomRepository;

    //todo later - inject user service
    public ChatroomService(ILogger<ChatroomService> logger, IChatroomRepository chatroomRepository)
    {
        _logger = logger;
        _chatroomRepository = chatroomRepository;
    }

    public async Task<Chatroom?> Get(string id)
    {
        _logger.LogInformation("Getting chatroom by id");

        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException($"{nameof(id)} is required");

        try
        {
            var chatroom = await _chatroomRepository.Get(id);

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

    public async Task<Chatroom> Create(CreateChatroomCommand command)
    {
        _logger.LogInformation("Creating chatroom");

        try
        {
            var newChatroom = NewChatroom.Create(command.Username);
            var chatroom = await _chatroomRepository.Create(newChatroom);
            _logger.LogInformation("Chatroom created");
            return chatroom;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not create chatroom");
            throw;
        }
    }

    public async Task Update(UpdateChatroomCommand command)
    {
        _logger.LogInformation("Updating chatroom");

        try
        {
            var chatroom = await _chatroomRepository.Get(command.ChatroomId) ??
            throw new Exception("Chatroom not found");

            //todo - perform updates

            await _chatroomRepository.Update(chatroom);
            _logger.LogInformation("Chatroom updated");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not update chatroom");
            throw;
        }
    }

    public async Task Delete(DeleteChatroomCommand command)
    {
        _logger.LogInformation("Deleting chatroom");

        try
        {
            var chatroom = await _chatroomRepository.Get(command.ChatroomId) ??
            throw new Exception("Chatroom not found");

            await _chatroomRepository.Delete(command.ChatroomId);
            _logger.LogInformation("Chatroom deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not delete chatroom");
            throw;
        }
    }
}