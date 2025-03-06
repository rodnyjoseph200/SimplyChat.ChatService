using ChatService.Core.Messages;
using ChatService.Core.Xes;

namespace ChatService.Core.ChatMessages.Models;

public abstract class ChatMessageBase
{
    public ID ChatroomId { get; protected set; }
    public ID UserId { get; protected set; }
    public string Content { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public ChatMessageTypes Type { get; protected set; }
    //todo
    //user base, empty means all users
    private readonly string[] Visibility = [];

    protected ChatMessageBase(ID chatRoomId, ID userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
    {
        X.StringIsNullOrWhitespace.ThrowArgumentException(
            (nameof(content), content));

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