using System.ComponentModel.DataAnnotations;
using ChatService.Core.ChatRooms.Commands;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public class UpdateChatRoomRequest
{
    [Required]
    public required string ChatRoomId { get; set; }

    internal static UpdateChatRoomCommand Convert(UpdateChatRoomRequest request) =>
        UpdateChatRoomCommand.Create(request.ChatRoomId);
}