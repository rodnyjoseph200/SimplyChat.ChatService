using ChatService.Core.Chatrooms.Models.Users;
using Newtonsoft.Json;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms.Models;

public record AzureCosmosDbChatroomUser(
    [JsonProperty("id")] string Id,
    [JsonProperty("username")] string Username,
    [JsonProperty("settings")] AzureCosmosDbChatRoomUserSettings Settings,
    [JsonProperty("isSuperUser")] bool IsSuperUser)
{
    private const string IdType = "chatroom-user";

    private static string GetId(Ulid ulid) => $"{IdType}-{ulid}";

    public static AzureCosmosDbChatroomUser Convert(ChatRoomUser chatRoomUser)
    {
        return new AzureCosmosDbChatroomUser(
            Id: chatRoomUser.Id.ToString(),
            Username: chatRoomUser.Username,
            Settings: AzureCosmosDbChatRoomUserSettings.Convert(chatRoomUser.Settings),
            IsSuperUser: chatRoomUser.IsSuperUser);
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