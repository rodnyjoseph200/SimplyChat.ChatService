using ChatService.Core.Chatrooms.SourceGeneration;
using ChatService.Core.ChatRooms.Commands;
using ChatService.Core.ChatRooms.Models;
using ChatService.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatService.Core.ChatRooms;

[Service]
public class ChatRoomService : IChatRoomService
{
    private readonly ILogger<ChatRoomService> _logger;
    private readonly IChatroomRepository _chatRoomRepository;

    public ChatRoomService(ILogger<ChatRoomService> logger, IChatroomRepository chatRoomRepository)
    {
        _logger = logger;
        _chatRoomRepository = chatRoomRepository;
    }

    public async Task<Chatroom?> Get(string id)
    {
        LogInfo.GettingChatroomById(_logger);

        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException($"{nameof(id)} is required");

        var chatroom = await _chatRoomRepository.Get(id);

        if (chatroom is not null)
            LogInfo.ChatroomFound(_logger);
        else
            LogInfo.ChatroomNotfound(_logger);

        return chatroom;
    }

    public async Task<Chatroom> Create(CreateChatRoomCommand command)
    {
        LogInfo.CreatingChatroom(_logger);

        var user = ChatRoomUser.CreateSuperUser(command.Username);
        var newChatRoom = NewChatroom.Create(user);
        var chatroom = await _chatRoomRepository.Create(newChatRoom);

        LogInfo.ChatroomCreated(_logger);
        return chatroom;
    }

    public async Task Update(UpdateChatRoomCommand command)
    {
        LogInfo.UpdatingChatroom(_logger);

        var chatRoom = await _chatRoomRepository.Get(command.ChatRoomId) ??
        throw new ResourceNotFoundException(nameof(Chatroom));

        //todo - perform updates

        await _chatRoomRepository.Update(chatRoom);
        LogInfo.ChatroomUpdated(_logger);
    }

    public async Task Delete(DeleteChatRoomCommand command)
    {
        LogInfo.DeletingChatroom(_logger);

        var chatRoom = await _chatRoomRepository.Get(command.ChatRoomId) ??
        throw new ResourceNotFoundException(nameof(Chatroom));

        await _chatRoomRepository.Delete(command.ChatRoomId);
        LogInfo.ChatroomDeleted(_logger);
    }
}