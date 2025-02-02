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

    private static string GenerateId() => Guid.NewGuid().ToString();

    public static ChatRoomUser Create(string username, bool isSuperUser = false) => new(GenerateId(), username, ChatRoomUserSettings.Create(ChatRoomColorSchemes.Light), isSuperUser);

    public static ChatRoomUser CreateSuperUser(string username) => Create(username, true);

    public static ChatRoomUser Load(string id, string username, ChatRoomUserSettings settings, bool isSuperUser) => new(id, username, settings, isSuperUser);
}