using ChatService.Core.ChatMessages.Commands;
using ChatService.Core.Messages;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models.Messages;

public record CreateChatMessageRequest(string UserId, string Content, DateTimeOffset CreatedAt, ChatMessageTypes Type)
{
    internal static CreateChatMessageCommand Convert(string chatroomId, CreateChatMessageRequest request) =>
        CreateChatMessageCommand.Create(chatroomId, request.UserId, request.Content, request.CreatedAt, request.Type);
}