using ChatService.Core.Chatrooms.Models.Users;
using Guarded.Guards;
using Newtonsoft.Json;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms.Models;

public record AzureCosmosDbChatroomUser(
    [property: JsonProperty("id")] string Id,
    [property: JsonProperty("username")] string Username,
    [property: JsonProperty("settings")] AzureCosmosDbChatRoomUserSettings Settings,
    [property: JsonProperty("isSuperUser")] bool IsSuperUser)
{
    private const string IdType = "chatroom-user";

    private static string GetId(Ulid ulid) => $"{IdType}-{ulid}";

    public static AzureCosmosDbChatroomUser Convert(ChatRoomUser chatRoomUser)
    {
        _ = Guard.AgainstNulls(chatRoomUser);

        return new AzureCosmosDbChatroomUser(
            Id: chatRoomUser.Id.ToString(),
            Username: chatRoomUser.Username,
            Settings: AzureCosmosDbChatRoomUserSettings.Convert(chatRoomUser.Settings),
            IsSuperUser: chatRoomUser.IsSuperUser);
    }

    public static ChatRoomUser Convert(AzureCosmosDbChatroomUser chatRoomUser)
    {
        _ = Guard.AgainstNulls(chatRoomUser);

        return ChatRoomUser.Load(
            chatRoomUser.Id,
            chatRoomUser.Username,
            AzureCosmosDbChatRoomUserSettings.Convert(chatRoomUser.Settings),
            chatRoomUser.IsSuperUser);
    }
}