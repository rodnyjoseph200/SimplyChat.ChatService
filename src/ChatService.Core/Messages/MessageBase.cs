namespace ChatService.Core.Messages;

public abstract class MessageBase
{
    protected string Id { get; set; }
    protected string ChatRoomId { get; set; }
    protected string UserId { get; set; }
    protected string Content { get; set; }
    protected DateTimeOffset CreatedAt { get; set; }
    protected MessageTypes Type { get; set; }

    protected MessageBase(string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
    {
        Id = Guid.NewGuid().ToString();
        ChatRoomId = chatRoomId;
        UserId = userId;
        Content = content;
        CreatedAt = createdAt;
        Type = (MessageTypes)type;
    }
}

