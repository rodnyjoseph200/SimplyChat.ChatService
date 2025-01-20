using ChatService.Core.ChatRoomMessages;

namespace ChatService.Core.Messages;

public class NewChatRoomMessage : ChatRoomMessageBase
{
    public NewChatRoomMessage(string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
        : base(chatRoomId, userId, content, createdAt, type)
    {
    }

    public static NewChatRoomMessage Create(string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
    {
        return new NewChatRoomMessage(chatRoomId, userId, content, createdAt, type);
    }
}