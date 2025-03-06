using ChatService.Core.ChatRooms.Commands;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public record UpdateChatRoomRequest(string ChatRoomId)
{
    public static UpdateChatRoomCommand Convert(UpdateChatRoomRequest request) =>
        UpdateChatRoomCommand.Create(request.ChatRoomId);
}