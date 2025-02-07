using System.ComponentModel.DataAnnotations;
using ChatService.Core.ChatMessages.Commands;

namespace ChatService.APIs.REST.Controllers.V1.ChatMessages.Models;

public class UpdateChatMessageRequest
{
    [Required]
    public required string Content { get; set; }

    internal static UpdateChatMessageCommand Convert(string chatroomId, string chatMessageId, UpdateChatMessageRequest request) =>
        UpdateChatMessageCommand.Create(chatroomId, chatMessageId, request.Content);
}