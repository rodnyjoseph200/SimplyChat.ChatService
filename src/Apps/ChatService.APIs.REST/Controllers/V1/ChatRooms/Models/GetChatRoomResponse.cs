using ChatService.Core.Chatrooms.Models.Users;
using ChatService.Core.ChatRooms.Models;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public record GetChatRoomResponse(string ChatRoomId, ChatRoomUsersResponse[] Users)
{
    public static GetChatRoomResponse Convert(Chatroom chatRoom) =>
        new(chatRoom.Id.ToString(), chatRoom.Users.Select(ChatRoomUsersResponse.Convert).ToArray());
}

public record ChatRoomUsersResponse(string Id, string Username, ChatRoomUserSettingsResponse Settings, bool IsSuperUser)
{
    public static ChatRoomUsersResponse Convert(ChatRoomUser chatRoomUser)
    {
        return new(
            Id: chatRoomUser.Id.ToString(),
            Username: chatRoomUser.Username,
            Settings: ChatRoomUserSettingsResponse.Convert(chatRoomUser.Settings),
            IsSuperUser: chatRoomUser.IsSuperUser);
    }
}

public record ChatRoomUserSettingsResponse(string ColorSchemes)
{
    public static ChatRoomUserSettingsResponse Convert(ChatRoomUserSettings userSettings) =>
        new(userSettings.Scheme.ToString());
}