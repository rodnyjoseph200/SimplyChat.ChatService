using ChatService.Core.Messages;
using Guarded.Guards;

namespace ChatService.Core.ChatMessages.Models;

public abstract record ChatMessageBase
{
    public ID ChatroomId { get; }
    public ID UserId { get; }
    public string Content { get; protected set; }
    public DateTimeOffset CreatedAt { get; }
    public ChatMessageTypes Type { get; }
    //todo
    //user base, empty means all users
    private readonly string[] Visibility = [];

    protected ChatMessageBase(ID chatRoomId, ID userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
    {
        _ = Guard.AgainstNullsAndWhitespaces(chatRoomId, nameof(chatRoomId));

        if (createdAt == default)
            throw new ArgumentException($"{nameof(createdAt)} is required.");

        //if (!Enum.IsDefined(typeof(ChatMessageTypes), type))
        //    throw new ArgumentException($"{nameof(type)} {type} is invalid.");

        ChatroomId = chatRoomId;
        UserId = userId;
        Content = content;
        CreatedAt = createdAt;
        Type = type;
    }
}