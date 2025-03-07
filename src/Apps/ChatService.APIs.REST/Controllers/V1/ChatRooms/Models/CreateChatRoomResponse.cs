using ChatService.Core.Chatrooms.Models.Users;
using ChatService.Core.ChatRooms.Models;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public record CreateChatRoomResponse(string ChatroomId, string SuperUserId, string SuperUserName)
{
    public static CreateChatRoomResponse Convert(Chatroom chatroom, ChatRoomUser superUser) =>
        new(chatroom.Id.ToString(), superUser.Id.ToString(), superUser.Username);
}