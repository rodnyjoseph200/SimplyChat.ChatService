namespace ChatService.Core.ChatRooms.Models;

public class NewChatroom : ChatRoomBase
{
    private NewChatroom(string username) : base(username)
    {
    }

    public static NewChatroom Create(string username) => new(username);
}