using ChatService.Core.ChatMessages.Commands;
using ChatService.Core.ChatMessages.Models;

namespace ChatService.Core.ChatMessages;

public interface IChatMessageService
{
    Task<ChatMessage?> Get(ID chatroomId, ID chatMessagId);
    Task<IReadOnlyCollection<ChatMessage>> GetByChatRoomId(ID chatroomId);
    Task<ChatMessage> Create(CreateChatMessageCommand command);
    Task Update(UpdateChatMessageCommand command);
    Task Delete(DeleteChatMessageCommand command);
}