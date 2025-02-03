using System.ComponentModel.DataAnnotations;
using ChatService.Core.ChatMessages.Models;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models.Messages;

public class CreateChatMessageResponse
{
    [Required]
    public required string ChatMessageId { get; set; }
    [Required]
    public required string ChatroomId { get; set; }
    [Required]
    public required string UserId { get; set; }

    internal static CreateChatMessageResponse Convert(ChatMessage chatMessage) =>
        new()
        {
            ChatMessageId = chatMessage.Id,
            ChatroomId = chatMessage.ChatroomId,
            UserId = chatMessage.UserId,
        };
}