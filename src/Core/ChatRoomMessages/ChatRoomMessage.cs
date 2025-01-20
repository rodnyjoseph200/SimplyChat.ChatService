using ChatService.Core.ChatRoomMessages;
using Simply.Track;

namespace ChatService.Core.Messages;

public class ChatRoomMessage : ChatRoomMessageBase
{
    public Tracker Tracker { get; init; }
    public string Id { get; init; }

    private ChatRoomMessage(Tracker tracker, string id, string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
        : base(chatRoomId, userId, content, createdAt, type)
    {
        Tracker = tracker;
        Id = id;
    }

    public static ChatRoomMessage Load(Tracker tracker, string id, string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
    {
        return new ChatRoomMessage(tracker, id, chatRoomId, userId, content, createdAt, type);
    }
}