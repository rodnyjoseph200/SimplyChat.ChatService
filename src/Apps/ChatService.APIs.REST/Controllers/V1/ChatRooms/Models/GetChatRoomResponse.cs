using ChatService.Core.Chatrooms.Models;

namespace ChatService.APIs.REST.Controllers.V1.Chatrooms.Models;

public class GetChatroomResponse
{
    public string ChatroomId { get; set; }
    public ChatroomUsersResponse[] Users { get; set; }

    public GetChatroomResponse(Chatroom chatroom)
    {
        ChatroomId = chatroom.Id;
        Users = chatroom.Users.Select(ChatroomUsersResponse.Convert).ToArray();
    }

    public static GetChatroomResponse Convert(Chatroom chatrooms) =>
        new(chatrooms);
}

public class ChatroomUsersResponse
{
    public string Id { get; set; }
    public ChatroomUserSettingsResponse Settings { get; set; }
    public bool IsSuperUser { get; set; }

    public ChatroomUsersResponse(ChatroomUser chatroomUser)
    {
        Id = chatroomUser.Id;
        Settings = ChatroomUserSettingsResponse.Convert(chatroomUser.Settings);
        IsSuperUser = chatroomUser.IsSuperUser;
    }

    public static ChatroomUsersResponse Convert(ChatroomUser chatroomUser) =>
        new(chatroomUser);
}

public class ChatroomUserSettingsResponse
{
    public string ColorSchemes { get; set; }

    public ChatroomUserSettingsResponse(ChatroomUserSettings userSettings)
    {
        ColorSchemes = userSettings.Scheme.ToString();
    }

    public static ChatroomUserSettingsResponse Convert(ChatroomUserSettings userSettings) =>
        new(userSettings);
}