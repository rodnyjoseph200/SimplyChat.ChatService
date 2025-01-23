using ChatService.Core.Chatrooms.Commands;

namespace ChatService.APIs.REST.Controllers.V1.Chatrooms.Models;

public class CreateChatroomRequest
{
    public required string username { get; set; }

    public static CreateChatroomCommand Convert(CreateChatroomRequest request) =>
        CreateChatroomCommand.Create(request.username);
}