using ChatService.Core.Chatrooms.Commands;
using ChatService.Core.Chatrooms.Models;

namespace ChatService.Core.Chatrooms;

public interface IChatroomService
{
    Task<Chatroom?> Get(string id);
    Task<Chatroom> Create(CreateChatroomCommand command);
    Task Update(UpdateChatroomCommand command);
    Task Delete(DeleteChatroomCommand command);
}