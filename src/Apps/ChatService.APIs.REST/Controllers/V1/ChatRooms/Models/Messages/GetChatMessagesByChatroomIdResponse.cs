using System.ComponentModel.DataAnnotations;
using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models.Messages;

public record GetChatMessagesByChatroomIdResponse([Required] string ChatRoomId, [Required] IReadOnlyCollection<ChatMessageResponse> ChatMessages)
{
    internal static GetChatMessagesByChatroomIdResponse Convert(string chatroomId, IReadOnlyCollection<ChatMessage> chatMessages)
    {
        return new(chatroomId,
            chatMessages.Select(chatMessage => new ChatMessageResponse(
            chatMessage.Id.ToString(),
            chatMessage.ChatroomId.ToString(),
            chatMessage.UserId.ToString(),
            chatMessage.Content,
            chatMessage.CreatedAt,
            chatMessage.Type
        )).ToList());
    }
}

public record ChatMessageResponse(string Id, string ChatRoomId, string UserId, string Content, DateTimeOffset CreatedAt, ChatMessageTypes Type)
{
    internal static ChatMessageResponse Convert(ChatMessage chatMessage)
    {
        return new(
            Id: chatMessage.Id.ToString(),
            ChatRoomId: chatMessage.ChatroomId.ToString(),
            UserId: chatMessage.UserId.ToString(),
            Content: chatMessage.Content,
            CreatedAt: chatMessage.CreatedAt,
            Type: chatMessage.Type);
    }
}