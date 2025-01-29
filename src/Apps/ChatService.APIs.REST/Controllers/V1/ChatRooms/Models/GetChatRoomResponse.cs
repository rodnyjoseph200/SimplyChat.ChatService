using System.ComponentModel.DataAnnotations;
using ChatService.Core.ChatRooms.Models;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public class GetChatRoomResponse
{
    [Required]
    public string ChatRoomId { get; init; }
    [Required]
    public ChatRoomUsersResponse[] Users { get; init; }

    private GetChatRoomResponse(Chatroom chatRoom)
    {
        ChatRoomId = chatRoom.Id;
        Users = chatRoom.Users.Select(ChatRoomUsersResponse.Convert).ToArray();
    }

    public static GetChatRoomResponse Convert(Chatroom chatRooms) =>
        new(chatRooms);
}

public class ChatRoomUsersResponse
{
    [Required]
    public string Id { get; init; }
    [Required]
    public string Username { get; init; }
    [Required]
    public ChatRoomUserSettingsResponse Settings { get; init; }
    [Required]
    public bool IsSuperUser { get; init; }

    private ChatRoomUsersResponse(ChatRoomUser chatRoomUser)
    {
        Id = chatRoomUser.Id;
        Username = chatRoomUser.Username;
        Settings = ChatRoomUserSettingsResponse.Convert(chatRoomUser.Settings);
        IsSuperUser = chatRoomUser.IsSuperUser;
    }

    public static ChatRoomUsersResponse Convert(ChatRoomUser chatRoomUser) =>
        new(chatRoomUser);
}

public class ChatRoomUserSettingsResponse
{
    [Required]
    public string ColorSchemes { get; init; }

    private ChatRoomUserSettingsResponse(ChatRoomUserSettings userSettings)
    {
        ColorSchemes = userSettings.Scheme.ToString();
    }

    public static ChatRoomUserSettingsResponse Convert(ChatRoomUserSettings userSettings) =>
        new(userSettings);
}