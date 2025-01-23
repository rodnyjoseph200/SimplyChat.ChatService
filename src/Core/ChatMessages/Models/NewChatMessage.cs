using ChatService.Core.Messages;

namespace ChatService.Core.ChatMessages.Models;

public class NewChatMessage : ChatMessageBase
{
    public NewChatMessage(string chatRoomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
        : base(chatRoomId, userId, content, createdAt, type)
    {
    }

    public static NewChatMessage Create(string chatRoomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
    {
        return new NewChatMessage(chatRoomId, userId, content, createdAt, type);
    }
}