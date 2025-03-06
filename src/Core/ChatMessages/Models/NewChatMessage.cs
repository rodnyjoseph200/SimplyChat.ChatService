using ChatService.Core.Messages;

namespace ChatService.Core.ChatMessages.Models;

public class NewChatMessage : ChatMessageBase
{
    public NewChatMessage(ID chatRoomId, ID userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
        : base(chatRoomId, userId, content, createdAt, type)
    {
    }

    public static NewChatMessage Create(ID chatRoomId, ID userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
    {
        return new NewChatMessage(chatRoomId, userId, content, createdAt, type);
    }
}