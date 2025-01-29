
using ChatService.Core.ChatMessages.Commands;
using ChatService.Core.Messages;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models.Messages;

public class CreateChatMessageRequest
{
    public required string UserId { get; set; }
    public required string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public ChatMessageTypes Type { get; set; }

    internal static CreateChatMessageCommand Convert(string chatroomId, CreateChatMessageRequest request) =>
        CreateChatMessageCommand.Create(chatroomId, request.UserId, request.Content, request.CreatedAt, request.Type);
}