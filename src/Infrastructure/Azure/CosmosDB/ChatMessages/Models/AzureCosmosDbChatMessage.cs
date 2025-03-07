using ChatService.Core;
using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;
using Guarded.Guards;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Simply.Track;

namespace ChatService.Infrastructure.Azure.CosmosDB.ChatMessages.Models;

public record AzureCosmosDbChatMessage(
    [property: JsonProperty("id")] string Id,
    [property: JsonProperty("partitionKey")] string PartitionKey,
    [property: JsonProperty("chatroomId")] string ChatroomId,
    [property: JsonProperty("userId")] string UserId,
    [property: JsonProperty("content")] string Content,
    [property: JsonProperty("createdAt")] DateTimeOffset CreatedAt,
    [property: JsonProperty("type")] ChatMessageTypes Type,
    [property: JsonProperty("tracker")] DbTracker Tracker)
{
    private const string IdType = "chatroom-message";

    public static PartitionKey GetPartitionKey(ID chatroomId) => new(chatroomId.ToString());

    public static PartitionKey GetPartitionKey(NewChatMessage newChatMessage) => new(newChatMessage.ChatroomId.ToString());

    public static PartitionKey GetPartitionKey(ChatMessage chatMessage) => new(chatMessage.ChatroomId.ToString());

    public static AzureCosmosDbChatMessage Convert(NewChatMessage newChatMessage)
    {
        _ = Guard.AgainstNulls(newChatMessage);

        return new AzureCosmosDbChatMessage(
            Id: ID.Generate.ToString(),
            PartitionKey: newChatMessage.ChatroomId.ToString(),
            ChatroomId: newChatMessage.ChatroomId.ToString(),
            UserId: newChatMessage.UserId.ToString(),
            Content: newChatMessage.Content,
            CreatedAt: newChatMessage.CreatedAt,
            Type: newChatMessage.Type,
            Tracker: DbTracker.Create(newChatMessage.UserId.ToString()));
    }

    public static AzureCosmosDbChatMessage Update(ChatMessage chatMessage)
    {
        _ = Guard.AgainstNulls(chatMessage);

        return new AzureCosmosDbChatMessage(
            Id: chatMessage.Id.ToString(),
            PartitionKey: chatMessage.ChatroomId.ToString(),
            ChatroomId: chatMessage.ChatroomId.ToString(),
            UserId: chatMessage.UserId.ToString(),
            Content: chatMessage.Content,
            CreatedAt: chatMessage.CreatedAt,
            Type: chatMessage.Type,
            Tracker: DbTracker.Update(chatMessage.Tracker, chatMessage.UserId.ToString()));
    }

    public static ChatMessage Convert(AzureCosmosDbChatMessage azureCosmosDbChatMessage)
    {
        _ = Guard.AgainstNulls(azureCosmosDbChatMessage);

        return ChatMessage.Load(
            DbTracker.Convert(azureCosmosDbChatMessage.Tracker),
            azureCosmosDbChatMessage.Id,
            azureCosmosDbChatMessage.ChatroomId,
            azureCosmosDbChatMessage.UserId,
            azureCosmosDbChatMessage.Content,
            azureCosmosDbChatMessage.CreatedAt,
            azureCosmosDbChatMessage.Type);
    }

    public static ChatMessage[] Convert(List<AzureCosmosDbChatMessage> azureCosmosDbChatMessage)
    {
        _ = Guard.AgainstNulls(azureCosmosDbChatMessage);

        var chatMessages = new List<ChatMessage>();
        foreach (var message in azureCosmosDbChatMessage)
        {
            if (message is null)
                continue;

            chatMessages.Add(Convert(message));
        }

        return chatMessages.ToArray();
    }
}