using ChatService.Core.ChatRooms.Models;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public class GetChatRoomResponse
{
    public string ChatRoomId { get; set; }
    public ChatRoomUsersResponse[] Users { get; set; }

    public GetChatRoomResponse(ChatRoom chatRoom)
    {
        ChatRoomId = chatRoom.Id;
        Users = chatRoom.Users.Select(ChatRoomUsersResponse.Convert).ToArray();
    }

    public static GetChatRoomResponse Convert(ChatRoom chatRooms) =>
        new(chatRooms);
}

public class ChatRoomUsersResponse
{
    public string Id { get; set; }
    public ChatRoomUserSettingsResponse Settings { get; set; }
    public bool IsSuperUser { get; set; }

    public ChatRoomUsersResponse(ChatRoomUser chatRoomUser)
    {
        Id = chatRoomUser.Id;
        Settings = ChatRoomUserSettingsResponse.Convert(chatRoomUser.Settings);
        IsSuperUser = chatRoomUser.IsSuperUser;
    }

    public static ChatRoomUsersResponse Convert(ChatRoomUser chatRoomUser) =>
        new(chatRoomUser);
}

public class ChatRoomUserSettingsResponse
{
    public string ColorSchemes { get; set; }

    public ChatRoomUserSettingsResponse(ChatRoomUserSettings userSettings)
    {
        ColorSchemes = userSettings.Scheme.ToString();
    }

    public static ChatRoomUserSettingsResponse Convert(ChatRoomUserSettings userSettings) =>
        new(userSettings);
}

