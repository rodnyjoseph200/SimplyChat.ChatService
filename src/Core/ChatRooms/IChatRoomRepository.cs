using ChatService.Core.ChatRooms.Models;

namespace ChatService.Core.ChatRooms;

public interface IChatRoomRepository
{
    Task<ChatRoom> Create(NewChatRoom newChatRoom);
    Task<ChatRoom?> Get(string id);
    Task Update(ChatRoom chatRoom);
    Task Delete(string chatRoomId);
}