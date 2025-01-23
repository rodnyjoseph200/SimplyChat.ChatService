using ChatService.Core.Chatrooms.Models;

namespace ChatService.Core.Chatrooms;

public interface IChatroomRepository
{
    Task<Chatroom?> Get(string id);
    Task<Chatroom> Create(NewChatroom newChatroom);
    Task Update(Chatroom chatroom);
    Task Delete(string chatroomId);
}