﻿using ChatService.Core.Messages;
using Guarded.Guards;
using Simply.Track;

namespace ChatService.Core.ChatMessages.Models;

public record ChatMessage : ChatMessageBase
{
    public ID Id { get; }
    public Tracker Tracker { get; }

    private ChatMessage(Tracker tracker, ID id, ID chatRoomId, ID userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
        : base(chatRoomId, userId, content, createdAt, type)
    {
        _ = Guard.AgainstNulls(tracker);

        Id = id;
        Tracker = tracker;
    }

    public static ChatMessage Load(Tracker tracker, ID id, ID chatRoomId, ID userId, string content, DateTimeOffset createdAt, ChatMessageTypes type) =>
        new(tracker, id, chatRoomId, userId, content, createdAt, type);

    public void UpdateContent(string content)
    {
        _ = Guard.AgainstNullsAndWhitespaces(content);
        Content = content;
    }
}