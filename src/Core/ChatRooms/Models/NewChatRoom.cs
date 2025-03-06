using ChatService.Core.Chatrooms.Models.Users;

namespace ChatService.Core.ChatRooms.Models;

public class NewChatroom : ChatRoomBase
{
    private NewChatroom(ChatRoomUser chatroomUser) : base(chatroomUser)
    {
    }

    public static NewChatroom Create(ChatRoomUser chatroomUser) => new(chatroomUser);
}