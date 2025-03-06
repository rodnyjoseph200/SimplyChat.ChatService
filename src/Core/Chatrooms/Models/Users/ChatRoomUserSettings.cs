using ChatService.Core.ChatRooms;

namespace ChatService.Core.Chatrooms.Models.Users;

public class ChatRoomUserSettings
{
    public ChatRoomColorSchemes Scheme { get; private set; }

    private ChatRoomUserSettings(ChatRoomColorSchemes scheme) => Scheme = scheme;

    public static ChatRoomUserSettings Create(ChatRoomColorSchemes scheme = ChatRoomColorSchemes.Light) => new(scheme);

    public static ChatRoomUserSettings Load(ChatRoomColorSchemes scheme) => new(scheme);

    public void SetScheme(ChatRoomColorSchemes scheme) => Scheme = scheme;
}