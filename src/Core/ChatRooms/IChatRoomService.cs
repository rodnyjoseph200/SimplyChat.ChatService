using ChatService.Core.ChatRooms.Commands;
using ChatService.Core.ChatRooms.Models;

namespace ChatService.Core.ChatRooms;

public interface IChatRoomService
{
    Task<ChatRoom?> Get(string id);
    Task<ChatRoom> Create(CreateChatRoomCommand command);
    Task Update(UpdateChatRoomCommand command);
    Task Delete(DeleteChatRoomCommand command);
}