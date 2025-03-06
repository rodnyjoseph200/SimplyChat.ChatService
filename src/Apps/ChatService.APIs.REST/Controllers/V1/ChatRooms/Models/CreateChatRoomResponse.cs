using System.ComponentModel.DataAnnotations;
using ChatService.Core;
using ChatService.Core.Chatrooms.Models.Users;
using ChatService.Core.ChatRooms.Models;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public class CreateChatRoomResponse
{
    [Required]
    public string ChatroomId { get; init; }
    [Required]
    public string SuperUserId { get; set; }
    [Required]
    public string SuperUserName { get; init; }

    private CreateChatRoomResponse(ID chatroomId, ChatRoomUser superUser)
    {
        ChatroomId = chatroomId.ToString();
        SuperUserId = superUser.Id.ToString();
        SuperUserName = superUser.Username;
    }

    public static CreateChatRoomResponse Convert(Chatroom chatroom, ChatRoomUser superUser) =>
        new(chatroom.Id, superUser);
}