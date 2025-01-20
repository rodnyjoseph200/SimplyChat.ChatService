using ChatService.Core.Messages;
using Microsoft.Azure.Cosmos;
using Simply.Track;

namespace ChatService.Infrastructure.Azure.CosmosDb.Messages;

public class CosmosDbChatRoomMessage
{
    private const string PARTITION_KEY_PREFIX = "Partition#Message";
    public required string PartitionKey { get; set; }
    public required string Id { get; set; }
    public required string ChatRoomId { get; set; }
    public required string UserId { get; set; }
    public required string Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public ChatRoomMessageTypes Type { get; set; }
    //todo
    //user base, empty means all users
    //public readonly string[] Visibility = [];
    public required DbTracker Tracker { get; set; }

    public PartitionKey PartitionKeyTest => new("todo");

    internal static PartitionKey BuildPartitionKey(CosmosDbChatRoomMessagePartitionType type, string id) =>
        string.IsNullOrWhiteSpace(id) ?
        throw new ArgumentException($"{nameof(id)} is required") :
        new PartitionKey($"{PARTITION_KEY_PREFIX}-{type}-{id}");

    public static CosmosDbChatRoomMessage Convert(NewChatRoomMessage message, CosmosDbChatRoomMessagePartitionType type)
    {
        return new CosmosDbChatRoomMessage
        {
            Id = message.Id,
            ChatRoomId = message.ChatRoomId,
            UserId = message.UserId,
            Content = message.Content,
            CreatedAt = message.CreatedAt,
            Type = message.Type,
            Tracker = DbTracker.Create(message.UserId),
            PartitionKey = $"{PARTITION_KEY_PREFIX}-{type}-{id}"
        }
    }

    public static ChatRoomMessage Convert(CosmosDbChatRoomMessage message)
    {
        throw new NotImplementedException();
    }
}
