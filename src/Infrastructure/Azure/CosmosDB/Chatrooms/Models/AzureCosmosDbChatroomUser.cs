using ChatService.Core.ChatRooms.Models;
using Newtonsoft.Json;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms.Models;

public class AzureCosmosDbChatroomUser
{
    [JsonProperty("id")]
    public required string Id { get; set; }

    [JsonProperty("username")]
    public required string Username { get; set; }

    [JsonProperty("settings")]
    public required AzureCosmosDbChatRoomUserSettings Settings { get; set; }

    [JsonProperty("isSuperUser")]
    public required bool IsSuperUser { get; set; }

    public static AzureCosmosDbChatroomUser Convert(ChatRoomUser chatRoomUser)
    {
        return new AzureCosmosDbChatroomUser
        {
            Id = chatRoomUser.Id,
            Username = chatRoomUser.Username,
            Settings = AzureCosmosDbChatRoomUserSettings.Convert(chatRoomUser.Settings),
            IsSuperUser = chatRoomUser.IsSuperUser
        };
    }

    public static ChatRoomUser Convert(AzureCosmosDbChatroomUser newChatRoomUser)
    {
        return ChatRoomUser.Load(
            newChatRoomUser.Id,
            newChatRoomUser.Username,
            AzureCosmosDbChatRoomUserSettings.Convert(newChatRoomUser.Settings),
            newChatRoomUser.IsSuperUser);
    }
}