using ChatService.Core.ChatRooms.Commands;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public record CreateChatRoomRequest(string Username)
{
    public static CreateChatRoomCommand Convert(CreateChatRoomRequest request) =>
        CreateChatRoomCommand.Create(request.Username);
}