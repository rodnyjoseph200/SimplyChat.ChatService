using ChatService.Core.ChatMessages.Commands;

namespace ChatService.APIs.REST.Controllers.V1.ChatMessages.Models;

public class UpdateChatMessageRequest
{
    public required string ChatMessageId { get; set; }
    public required string Content { get; set; }

    internal static UpdateChatMessageCommand Convert(UpdateChatMessageRequest request) =>
        UpdateChatMessageCommand.Create(request.ChatMessageId, request.Content);
}