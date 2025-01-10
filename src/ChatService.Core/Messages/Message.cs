using Simply.Track;

namespace ChatService.Core.Messages;

public class Message : MessageBase
{
    public Tracker Tracker { get; init; }

    private Message(Tracker tracker, string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
        : base(chatRoomId, userId, content, createdAt, type)
    {
        Tracker = tracker;
    }

    public static Message Load(Tracker tracker, string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
    {
        return new Message(tracker, chatRoomId, userId, content, createdAt, type);
    }
}