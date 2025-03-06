using ChatService.Core.Chatrooms.Models.Users;
using Newtonsoft.Json;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms.Models;

public class AzureCosmosDbChatroomUser
{
    private const string IdType = "chatroom-user";

    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("username")]
    public required string Username { get; set; }

    [JsonProperty("settings")]
    public required AzureCosmosDbChatRoomUserSettings Settings { get; set; }

    [JsonProperty("isSuperUser")]
    public required bool IsSuperUser { get; set; }

    private static string GetId(Ulid ulid) => $"{IdType}-{ulid}";

    public static AzureCosmosDbChatroomUser Convert(ChatRoomUser chatRoomUser)
    {
        return new AzureCosmosDbChatroomUser
        {
            Id = chatRoomUser.Id.ToString(),
            Username = chatRoomUser.Username,
            Settings = AzureCosmosDbChatRoomUserSettings.Convert(chatRoomUser.Settings),
            IsSuperUser = chatRoomUser.IsSuperUser
        };
    }

    public static ChatRoomUser Convert(AzureCosmosDbChatroomUser chatRoomUser)
    {
        return ChatRoomUser.Load(
            chatRoomUser.Id,
            chatRoomUser.Username,
            AzureCosmosDbChatRoomUserSettings.Convert(chatRoomUser.Settings),
            chatRoomUser.IsSuperUser);
    }
}