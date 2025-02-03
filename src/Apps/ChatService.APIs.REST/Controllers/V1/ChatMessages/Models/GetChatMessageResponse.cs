using System.ComponentModel.DataAnnotations;
using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;

namespace ChatService.APIs.REST.Controllers.V1.ChatMessages.Models;

public class GetChatMessageResponse
{
    [Required]
    public string ChatMessageId { get; init; }
    [Required]
    public string ChatroomId { get; init; }
    [Required]
    public string UserId { get; init; }
    [Required]
    public string Content { get; init; }
    [Required]
    public DateTimeOffset CreatedAt { get; init; }
    [Required]
    public ChatMessageTypes Type { get; init; }

    private GetChatMessageResponse(string chatMessageId, string chatroomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
    {
        ChatMessageId = chatMessageId;
        ChatroomId = chatroomId;
        UserId = userId;
        Content = content;
        CreatedAt = createdAt;
        Type = type;
    }
    public static GetChatMessageResponse Convert(ChatMessage chatMessage) =>
        new(chatMessage.Id, chatMessage.ChatroomId, chatMessage.UserId, chatMessage.Content, chatMessage.CreatedAt, chatMessage.Type);
}