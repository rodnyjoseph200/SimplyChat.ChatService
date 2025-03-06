using ChatService.Core;
using ChatService.Core.ChatRooms.Models;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Simply.Track;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms.Models;

public class AzureCosmosDbChatroom
{
    private const string EntityType = "chatroom";

    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("partitionKey")]
    public required string PartitionKey { get; set; }

    [JsonProperty("users")]
    public required IReadOnlyCollection<AzureCosmosDbChatroomUser> Users { get; set; }

    [JsonProperty("tracker")]
    public required DbTracker Tracker { get; set; }

    public static string GetPartitionKeyString(ID chatroomId)
    {
        var date = chatroomId.Value.Time.ToString("yyyy-MM-dd");
        return $"{EntityType}-{date}";
    }

    public static PartitionKey GetPartitionKey(ID chatroomId) => new(GetPartitionKeyString(chatroomId));

    public static PartitionKey GetPartitionKey(Chatroom chatroom) => new(GetPartitionKeyString(chatroom.Id));

    public static Chatroom Convert(AzureCosmosDbChatroom dbChatroom)
    {
        return Chatroom.Load(
            dbChatroom.Id,
            dbChatroom.Users.Select(AzureCosmosDbChatroomUser.Convert).ToList(),
            DbTracker.Convert(dbChatroom.Tracker));
    }

    public static AzureCosmosDbChatroom Convert(NewChatroom newChatroom)
    {
        var superUser = newChatroom.Users.Single();
        var id = ID.Generate;

        return new AzureCosmosDbChatroom
        {
            Id = id.ToString(),
            PartitionKey = GetPartitionKeyString(id),
            Users = newChatroom.Users.Select(AzureCosmosDbChatroomUser.Convert).ToList(),
            Tracker = DbTracker.Create(superUser.Username) //todo - overload (username, datetimeoffset as created at)
        };
    }

    public static AzureCosmosDbChatroom Update(Chatroom chatroom)
    {
        return new AzureCosmosDbChatroom
        {
            Id = chatroom.Id.ToString(),
            PartitionKey = GetPartitionKeyString(chatroom.Id),
            Users = chatroom.Users.Select(AzureCosmosDbChatroomUser.Convert).ToList(),
            //todo, updated by
            Tracker = DbTracker.Update(chatroom.Tracker, chatroom.Users.First().Username)
        };
    }
}