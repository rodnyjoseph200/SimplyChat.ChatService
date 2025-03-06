using ChatService.Core.ChatMessages.Models;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models.Messages;

public record CreateChatMessageResponse(string ChatMessageId, string ChatroomId, string UserId)
{
    internal static CreateChatMessageResponse Convert(ChatMessage chatMessage) =>
        new(chatMessage.Id.ToString(), chatMessage.ChatroomId.ToString(), chatMessage.UserId.ToString());
}