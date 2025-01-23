namespace ChatService.Core.Chatrooms.Models;

public class ChatroomUser
{
    public string Id { get; init; }
    public ChatroomUserSettings Settings { get; init; }
    public bool IsSuperUser { get; init; }

    private ChatroomUser(string id, ChatroomUserSettings settings, bool isSuperUser)
    {
        Id = id;
        Settings = settings;
        IsSuperUser = isSuperUser;
    }

    public static ChatroomUser Create(string id, ChatroomUserSettings settings) => new(id, settings, false);

    public static ChatroomUser CreateSuperUser(string id, ChatroomUserSettings settings) => new(id, settings, true);

    public static ChatroomUser Load(string id, ChatroomUserSettings settings, bool isSuperUser) => new(id, settings, isSuperUser);
}