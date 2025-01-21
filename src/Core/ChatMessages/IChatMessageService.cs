using ChatService.Core.ChatMessages.Commands;
using ChatService.Core.ChatMessages.Models;

namespace ChatService.Core.ChatMessages;

public interface IChatMessageService
{
    Task<ChatMessage?> Get(string chatMessagId);
    Task<IReadOnlyCollection<ChatMessage>> GetByChatRoomId(string chatroomId);
    Task<ChatMessage> Create(CreateChatMessageCommand command);
    Task Update(UpdateChatMessageCommand command);
    Task Delete(DeleteChatMessageCommand command);
}
