using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;

namespace ChatService.APIs.REST.Controllers.V1.ChatMessages.Models;

public record GetChatMessageResponse(string ChatMessageId, string ChatroomId, string UserId,
    string Content, DateTimeOffset CreatedAt, ChatMessageTypes Type)
{
    public static GetChatMessageResponse Convert(ChatMessage chatMessage)
    {
        return new GetChatMessageResponse(
            ChatMessageId: chatMessage.Id.ToString(),
            ChatroomId: chatMessage.ChatroomId.ToString(),
            UserId: chatMessage.UserId.ToString(),
            Content: chatMessage.Content,
            CreatedAt: chatMessage.CreatedAt,
            Type: chatMessage.Type);
    }
}