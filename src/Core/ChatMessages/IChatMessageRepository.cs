using ChatService.Core.ChatMessages.Models;

namespace ChatService.Core.ChatMessages;
public interface IChatMessageRepository
{
    Task<ChatMessage?> Get(ID chatroomId, ID chatMessageId);
    Task<IReadOnlyCollection<ChatMessage>> GetByChatRoomId(ID chatroomId);
    Task<ChatMessage> Create(NewChatMessage newChatMessage);
    Task Update(ChatMessage chatMessage);
    Task Delete(ID chatroomId, ID chatMessageId);
}