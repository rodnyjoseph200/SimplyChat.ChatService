namespace ChatService.Core.Chatrooms.Models;

public class ChatroomUserSettings
{
    public ChatroomColorSchemes Scheme { get; private set; }

    private ChatroomUserSettings(ChatroomColorSchemes scheme) => Scheme = scheme;

    public static ChatroomUserSettings Create(ChatroomColorSchemes scheme = ChatroomColorSchemes.Light) => new(scheme);

    public static ChatroomUserSettings Load(ChatroomColorSchemes scheme) => new(scheme);

    public void SetScheme(ChatroomColorSchemes scheme) => Scheme = scheme;
}