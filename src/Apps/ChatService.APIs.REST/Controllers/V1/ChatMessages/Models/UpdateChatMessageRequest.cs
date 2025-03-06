using ChatService.Core.ChatMessages.Commands;

namespace ChatService.APIs.REST.Controllers.V1.ChatMessages.Models;

public record UpdateChatMessageRequest(string Content)
{
    public static UpdateChatMessageCommand Convert(string chatroomId, string chatMessageId, UpdateChatMessageRequest request) =>
        UpdateChatMessageCommand.Create(chatroomId, chatMessageId, request.Content);
}