using ChatService.Core;
using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Simply.Track;

namespace ChatService.Infrastructure.Azure.CosmosDB.ChatMessages.Models;

public class AzureCosmosDbChatMessage
{
    private const string IdType = "chatroom-message";

    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("partitionKey")]
    public required string PartitionKey { get; set; }

    [JsonProperty("chatroomId")]
    public required string ChatroomId { get; set; }

    [JsonProperty("userId")]
    public required string UserId { get; set; }

    [JsonProperty("content")]
    public required string Content { get; set; }

    [JsonProperty("createdAt")]
    public required DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("type")]
    public required ChatMessageTypes Type { get; set; }

    [JsonProperty("tracker")]
    public required DbTracker Tracker { get; set; }

    public static PartitionKey GetPartitionKey(ID chatroomId) => new(chatroomId.ToString());

    public static PartitionKey GetPartitionKey(NewChatMessage newChatMessage) => new(newChatMessage.ChatroomId.ToString());

    public static PartitionKey GetPartitionKey(ChatMessage chatMessage) => new(chatMessage.ChatroomId.ToString());

    public static AzureCosmosDbChatMessage Convert(NewChatMessage newChatMessage)
    {
        return new AzureCosmosDbChatMessage
        {
            Id = Guid.NewGuid().ToString(),
            PartitionKey = newChatMessage.ChatroomId.ToString(),
            ChatroomId = newChatMessage.ChatroomId.ToString(),
            UserId = newChatMessage.UserId.ToString(),
            Content = newChatMessage.Content,
            CreatedAt = newChatMessage.CreatedAt,
            Type = newChatMessage.Type,
            Tracker = DbTracker.Create(newChatMessage.UserId.ToString())
        };
    }

    public static AzureCosmosDbChatMessage Update(ChatMessage chatMessage)
    {
        return new AzureCosmosDbChatMessage
        {
            Id = chatMessage.Id.ToString(),
            PartitionKey = chatMessage.ChatroomId.ToString(),
            ChatroomId = chatMessage.ChatroomId.ToString(),
            UserId = chatMessage.UserId.ToString(),
            Content = chatMessage.Content,
            CreatedAt = chatMessage.CreatedAt,
            Type = chatMessage.Type,
            Tracker = DbTracker.Update(chatMessage.Tracker, chatMessage.UserId.ToString())
        };
    }

    public static ChatMessage Convert(AzureCosmosDbChatMessage azureCosmosDbChatMessage)
    {
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