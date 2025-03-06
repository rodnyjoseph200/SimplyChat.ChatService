using ChatService.Core.Chatrooms.Models.Users;
using ChatService.Core.ChatRooms;
using Newtonsoft.Json;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms.Models;

public record AzureCosmosDbChatRoomUserSettings([JsonProperty("scheme")] string Scheme)
{
    public static AzureCosmosDbChatRoomUserSettings Convert(ChatRoomUserSettings chatRoomUserSettings) =>
        new(chatRoomUserSettings.Scheme.ToString());

    public static ChatRoomUserSettings Convert(AzureCosmosDbChatRoomUserSettings newChatRoomUserSettings) =>
        ChatRoomUserSettings.Load(Enum.Parse<ChatRoomColorSchemes>(newChatRoomUserSettings.Scheme));
}