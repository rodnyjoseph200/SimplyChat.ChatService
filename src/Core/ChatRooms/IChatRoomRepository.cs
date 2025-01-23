using ChatService.Core.ChatRooms.Models;

namespace ChatService.Core.ChatRooms;

public interface IChatroomRepository
{
    Task<Chatroom?> Get(string id);
    Task<Chatroom> Create(NewChatroom newChatRoom);
    Task Update(Chatroom chatRoom);
    Task Delete(string chatRoomId);
}