using System.ComponentModel.DataAnnotations;
using ChatService.Core.ChatRooms.Commands;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;

public class CreateChatRoomRequest
{
    [Required]
    public required string Username { get; set; }

    public static CreateChatRoomCommand Convert(CreateChatRoomRequest request) =>
        CreateChatRoomCommand.Create(request.Username);
}