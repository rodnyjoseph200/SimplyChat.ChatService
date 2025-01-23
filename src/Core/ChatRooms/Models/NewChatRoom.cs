namespace ChatService.Core.Chatrooms.Models;

public class NewChatroom : ChatroomBase
{
    private NewChatroom(string username) : base(username)
    {
    }

    public static NewChatroom Create(string username) => new(username);
}