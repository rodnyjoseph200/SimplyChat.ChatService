namespace ChatService.Core.ChatRooms.Models;

public class ChatRoomUser
{
    public string Id { get; init; }
    public ChatRoomUserSettings Settings { get; init; }
    public bool IsSuperUser { get; init; }

    private ChatRoomUser(string id, ChatRoomUserSettings settings, bool isSuperUser)
    {
        Id = id;
        Settings = settings;
        IsSuperUser = isSuperUser;
    }

    public static ChatRoomUser Create(string id, ChatRoomUserSettings settings) => new(id, settings, false);

    public static ChatRoomUser CreateSuperUser(string id, ChatRoomUserSettings settings) => new(id, settings, true);

    public static ChatRoomUser Load(string id, ChatRoomUserSettings settings, bool isSuperUser) => new(id, settings, isSuperUser);
}