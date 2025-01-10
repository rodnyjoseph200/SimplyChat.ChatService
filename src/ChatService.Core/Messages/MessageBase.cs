namespace ChatService.Core.Messages;

public abstract class MessageBase
{
    public string Id { get; protected set; }
    public string ChatRoomId { get; protected set; }
    public string UserId { get; protected set; }
    public string Content { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public MessageTypes Type { get; protected set; }

    protected MessageBase(string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
    {
        Simply.Lib.Simply.StringIsNullOrWhitespace.ThrowArgumentException(
            (nameof(chatRoomId), chatRoomId),
            (nameof(userId), userId),
            (nameof(content), content));

        if (createdAt == default)
            throw new ArgumentException($"{nameof(createdAt)} is required.");

        if (!Enum.IsDefined(typeof(MessageTypes), type))
            throw new ArgumentException($"{nameof(type)} {type} is invalid.");

        Id = Guid.NewGuid().ToString();
        ChatRoomId = chatRoomId;
        UserId = userId;
        Content = content;
        CreatedAt = createdAt;
        Type = (MessageTypes)type;
    }
}

