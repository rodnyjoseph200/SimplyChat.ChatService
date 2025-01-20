using ChatService.Core.Messages;

namespace ChatService.Core.ChatRoomMessages;

public abstract class ChatRoomMessageBase
{
    public string ChatRoomId { get; protected set; }
    public string UserId { get; protected set; }
    public string Content { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public ChatRoomMessageTypes Type { get; protected set; }
    //todo
    //user base, empty means all users
    private readonly string[] Visibility = [];

    protected ChatRoomMessageBase(string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
    {
        Simply.Lib.Simply.StringIsNullOrWhitespace.ThrowArgumentException(
            (nameof(chatRoomId), chatRoomId),
            (nameof(userId), userId),
            (nameof(content), content));

        if (createdAt == default)
            throw new ArgumentException($"{nameof(createdAt)} is required.");

        if (!Enum.IsDefined(typeof(ChatRoomMessageTypes), type))
            throw new ArgumentException($"{nameof(type)} {type} is invalid.");

        ChatRoomId = chatRoomId;
        UserId = userId;
        Content = content;
        CreatedAt = createdAt;
        Type = (ChatRoomMessageTypes)type;
    }
}