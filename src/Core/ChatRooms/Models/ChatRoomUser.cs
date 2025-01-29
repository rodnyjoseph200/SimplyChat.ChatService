namespace ChatService.Core.ChatRooms.Models;

public class ChatRoomUser
{
    public string Id { get; init; }
    public string Username { get; init; }
    public ChatRoomUserSettings Settings { get; init; }
    public bool IsSuperUser { get; init; }

    private ChatRoomUser(string id, string username, ChatRoomUserSettings settings, bool isSuperUser)
    {
        Id = id;
        Username = username;
        Settings = settings;
        IsSuperUser = isSuperUser;
    }

    public static ChatRoomUser Create(string id, string username, bool isSuperUser = false) => new(id, username, ChatRoomUserSettings.Create(ChatRoomColorSchemes.Light), false);

    public static ChatRoomUser CreateSuperUser(string id, string username) => Create(id, username, true);

    public static ChatRoomUser Load(string id, string username, ChatRoomUserSettings settings, bool isSuperUser) => new(id, username, settings, isSuperUser);
}