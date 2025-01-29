using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models.Messages;

public class GetChatMessagesByChatroomIdResponse
{
    public required string ChatRoomId { get; set; }
    public required IReadOnlyCollection<ChatMessageResponse> ChatMessages { get; set; }

    internal static object Convert(string chatroomId, IReadOnlyCollection<ChatMessage> chatMessages)
    {
        return new GetChatMessagesByChatroomIdResponse
        {
            ChatRoomId = chatroomId,
            ChatMessages = chatMessages.Select(chatMessage => new ChatMessageResponse
            {
                Id = chatMessage.Id,
                ChatRoomId = chatMessage.ChatroomId,
                UserId = chatMessage.UserId,
                Content = chatMessage.Content,
                CreatedAt = chatMessage.CreatedAt,
                Type = chatMessage.Type
            }).ToList()
        };
    }
}

public class ChatMessageResponse
{
    public required string Id { get; set; }
    public required string ChatRoomId { get; set; }
    public required string UserId { get; set; }
    public required string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public ChatMessageTypes Type { get; set; }
}