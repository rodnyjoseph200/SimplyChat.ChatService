using ChatService.Core.ChatRooms.Models;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Simply.Track;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms.Models;

public class AzureCosmosDbChatroom
{
    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("partitionKey")]
    public required string PartitionKey { get; set; }

    [JsonProperty("users")]
    public required IReadOnlyCollection<AzureCosmosDbChatroomUser> Users { get; set; }

    [JsonProperty("tracker")]
    public required DbTracker Tracker { get; set; }

    public static PartitionKey GetPartitionKey(string chatroomId) => new(chatroomId);

    public static PartitionKey GetPartitionKey(Chatroom chatroom) => new(chatroom.Id);

    public static Chatroom Convert(AzureCosmosDbChatroom dbChatroom)
    {
        return Chatroom.Load(
            dbChatroom.Id,
            dbChatroom.Users.Select(AzureCosmosDbChatroomUser.Convert).ToList(),
            DbTracker.Convert(dbChatroom.Tracker));
    }

    public static AzureCosmosDbChatroom Convert(NewChatroom chatroom)
    {
        var superUser = chatroom.Users.Single();
        var id = Guid.NewGuid().ToString();

        return new AzureCosmosDbChatroom
        {
            Id = id,
            PartitionKey = id,
            Users = chatroom.Users.Select(AzureCosmosDbChatroomUser.Convert).ToList(),
            Tracker = DbTracker.Create(superUser.Username)
        };
    }

    public static AzureCosmosDbChatroom Update(Chatroom chatroom)
    {
        return new AzureCosmosDbChatroom
        {
            Id = chatroom.Id,
            PartitionKey = chatroom.Id,
            Users = chatroom.Users.Select(AzureCosmosDbChatroomUser.Convert).ToList(),
            //todo, updated by
            Tracker = DbTracker.Update(chatroom.Tracker, chatroom.Users.First().Username)
        };
    }
}