using ChatService.Core.ChatMessages.Models;

namespace ChatService.Core.ChatMessages;
public interface IChatMessageRepository
{
    Task<ChatMessage?> Get(string id);
    Task<IReadOnlyCollection<ChatMessage>> GetByChatRoomId(string chatroomId);
    Task<ChatMessage> Create(NewChatMessage newChatMessage);
    Task Update(ChatMessage chatMessage);
    Task Delete(string id);
}
