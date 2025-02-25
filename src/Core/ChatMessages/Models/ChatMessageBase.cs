﻿using ChatService.Core.Messages;
using ChatService.Core.Xes;

namespace ChatService.Core.ChatMessages.Models;

public abstract class ChatMessageBase
{
    public string ChatroomId { get; protected set; }
    public string UserId { get; protected set; }
    public string Content { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public ChatMessageTypes Type { get; protected set; }
    //todo
    //user base, empty means all users
    private readonly string[] Visibility = [];

    protected ChatMessageBase(string chatRoomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
    {
        X.StringIsNullOrWhitespace.ThrowArgumentException(
            (nameof(chatRoomId), chatRoomId),
            (nameof(userId), userId),
            (nameof(content), content));

        if (createdAt == default)
            throw new ArgumentException($"{nameof(createdAt)} is required.");

        //if (!Enum.IsDefined(typeof(ChatMessageTypes), type))
        //    throw new ArgumentException($"{nameof(type)} {type} is invalid.");

        ChatroomId = chatRoomId;
        UserId = userId;
        Content = content;
        CreatedAt = createdAt;
        //Type = (ChatMessageTypes)type;
        Type = type;
    }
}