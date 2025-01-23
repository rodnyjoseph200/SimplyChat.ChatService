using ChatService.Core.Messages;

namespace ChatService.Core.ChatMessages.Models;

public class NewChatMessage : ChatMessageBase
{
    public NewChatMessage(string chatroomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
        : base(chatroomId, userId, content, createdAt, type)
    {
    }

    public static NewChatMessage Create(string chatroomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
    {
        return new NewChatMessage(chatroomId, userId, content, createdAt, type);
    }
}