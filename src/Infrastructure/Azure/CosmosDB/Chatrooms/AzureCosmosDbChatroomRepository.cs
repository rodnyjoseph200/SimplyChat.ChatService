using ChatService.Core.ChatRooms;
using ChatService.Core.ChatRooms.Models;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms;

public class AzureCosmosDbChatroomRepository : IChatroomRepository
{
    public Task<Chatroom?> Get(string id) => throw new NotImplementedException();
    public Task<Chatroom> Create(NewChatroom newChatroom) => throw new NotImplementedException();
    public Task Update(Chatroom chatroom) => throw new NotImplementedException();
    public Task Delete(string chatroomId) => throw new NotImplementedException();
}
