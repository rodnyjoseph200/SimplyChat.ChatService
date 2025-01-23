using ChatService.Core.ChatRooms.Commands;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public class CreateChatRoomRequest
{
    public required string username { get; set; }

    public static CreateChatRoomCommand Convert(CreateChatRoomRequest request) =>
        CreateChatRoomCommand.Create(request.username);
}