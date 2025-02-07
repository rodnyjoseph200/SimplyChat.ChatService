using Asp.Versioning;
using ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;
using ChatService.Core.ChatRooms;
using ChatService.Core.ChatRooms.Commands;
using ChatService.Core.ChatRooms.Models;
using Microsoft.AspNetCore.Mvc;
using Simply.Log;
using Swashbuckle.AspNetCore.Annotations;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms;

[ApiController]
[ApiVersion("1.0")]
[Route("api/chat-service/v{version:apiVersion}/[controller]")]
public class ChatRoomsController : ControllerBase
{
    private readonly ILogger<ChatRoomsController> _logger;
    private readonly IChatRoomService _chatRoomService;

    public ChatRoomsController(ILogger<ChatRoomsController> logger, IChatRoomService chatRoomService)
    {
        _logger = logger;
        _chatRoomService = chatRoomService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Get Chatroom by ID", Description = "Gets a chatroom by its ID")]
    public async Task<ActionResult<GetChatRoomResponse>> Get(string id)
    {
        using var _ = _logger.AddEntityId(typeof(Chatroom), id);
        _logger.LogInformation("Received request to get chatroom by id");

        var chatroom = await _chatRoomService.Get(id);
        _logger.LogInformation("Request to get chatroom by id completed");

        return chatroom is null ? NotFound() :
            GetChatRoomResponse.Convert(chatroom);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Create Chatroom", Description = "Creates a Chatroom")]
    public async Task<ActionResult<CreateChatRoomResponse>> Create([FromBody] CreateChatRoomRequest request)
    {
        using var _ = _logger.AddField($"{nameof(request.Username)}", request.Username);
        _logger.LogInformation("Received request to create chatroom");

        var command = CreateChatRoomRequest.Convert(request);
        var chatroom = await _chatRoomService.Create(command);

        _logger.LogInformation("Request to create chatroom completed");
        var response = CreateChatRoomResponse.Convert(chatroom, chatroom.SuperUser);
        return response;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Update Chatroom", Description = "Updates a Chatroom")]
    public async Task<ActionResult> Update([FromBody] UpdateChatRoomRequest request)
    {
        using var _ = _logger.AddField($"{nameof(request.ChatRoomId)}", request.ChatRoomId);
        _logger.LogInformation("Received request to update chatroom");

        var command = UpdateChatRoomRequest.Convert(request);
        await _chatRoomService.Update(command);

        _logger.LogInformation("Request to update chatroom completed");
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Delete Chatroom", Description = "Deletes a Chatroom")]
    public async Task<ActionResult> Delete(string id)
    {
        using var _ = _logger.AddField($"{nameof(id)}", id);
        _logger.LogInformation("Received request to delete chatroom");

        var command = DeleteChatRoomCommand.Create(id);
        await _chatRoomService.Delete(command);

        _logger.LogInformation("Request to delete chatroom completed");
        return NoContent();
    }

    //todo
    //chatrooms/{id}/users
    //get, add, update user name, delete
}