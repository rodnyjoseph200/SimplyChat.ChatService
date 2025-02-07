using ChatService.Core.ChatRooms;
using ChatService.Core.ChatRooms.Models;
using Newtonsoft.Json;

namespace ChatService.Infrastructure.Azure.CosmosDB.Chatrooms.Models;

public class AzureCosmosDbChatRoomUserSettings
{
    [JsonProperty("scheme")]
    public required string Scheme { get; set; }

    public static AzureCosmosDbChatRoomUserSettings Convert(ChatRoomUserSettings chatRoomUserSettings)
    {
        return new AzureCosmosDbChatRoomUserSettings
        {
            Scheme = chatRoomUserSettings.Scheme.ToString()
        };
    }

    public static ChatRoomUserSettings Convert(AzureCosmosDbChatRoomUserSettings newChatRoomUserSettings)
    {
        return ChatRoomUserSettings.Load(
            Enum.Parse<ChatRoomColorSchemes>(newChatRoomUserSettings.Scheme));
    }
}