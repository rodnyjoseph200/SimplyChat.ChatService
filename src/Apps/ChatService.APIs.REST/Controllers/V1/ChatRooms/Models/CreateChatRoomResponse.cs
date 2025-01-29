using System.ComponentModel.DataAnnotations;
using ChatService.Core.ChatRooms.Models;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public class CreateChatRoomResponse
{
    [Required]
    public string ChatroomId { get; init; }
    public string SuperUserId { get; set; }
    [Required]
    public string SuperUserName { get; init; }

    private CreateChatRoomResponse(string chatroomId, ChatRoomUser superUser)
    {
        ChatroomId = chatroomId;
        SuperUserId = superUser.Id;
        SuperUserName = superUser.Username;
    }

    public static CreateChatRoomResponse Convert(Chatroom chatroom, ChatRoomUser superUser) =>
        new(chatroom.Id, superUser);
}