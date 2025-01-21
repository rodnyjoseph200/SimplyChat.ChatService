namespace ChatService.Core.ChatRooms.Models;

public class NewChatRoom : ChatRoomBase
{
    private NewChatRoom(string username) : base(username)
    {
    }

    public static NewChatRoom Create(string username) => new(username);
}
