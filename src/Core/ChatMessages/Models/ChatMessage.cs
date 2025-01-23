using ChatService.Core.Messages;
using Simply.Track;

namespace ChatService.Core.ChatMessages.Models;

public class ChatMessage : ChatMessageBase
{
    public string Id { get; init; }
    public Tracker Tracker { get; init; }

    private ChatMessage(Tracker tracker, string id, string chatroomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
        : base(chatroomId, userId, content, createdAt, type)
    {
        Id = id;
        Tracker = tracker;
    }

    public static ChatMessage Load(Tracker tracker, string id, string chatroomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
    {
        return new ChatMessage(tracker, id, chatroomId, userId, content, createdAt, type);
    }

    public void UpdateContent(string content)
    {
        Content = content;
    }
}