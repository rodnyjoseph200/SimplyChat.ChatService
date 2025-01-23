using Asp.Versioning;
using ChatService.APIs.REST.Controllers.V1.ChatRooms.Models;
using ChatService.APIs.REST.Controllers.V1.ChatRooms.Models.Messages;
using ChatService.Core.ChatMessages;
using ChatService.Core.ChatRooms;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.APIs.REST.Controllers.V1.ChatRooms;

[ApiController]
[ApiVersion("1.0")]
[Route("api/chat-service/v{version:apiVersion}/[controller]")]
public class ChatRoomsController : ControllerBase
{
    private readonly ILogger<ChatRoomsController> _logger;
    private readonly IChatRoomService _chatRoomService;
    private readonly IChatMessageService _chatMessageService;

    public ChatRoomsController(ILogger<ChatRoomsController> logger, IChatRoomService chatRoomService, IChatMessageService messageService)
    {
        _logger = logger;
        _chatRoomService = chatRoomService;
        _chatMessageService = messageService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetChatRoomResponse>> GetChatRoom(string id)
    {
        _logger.LogInformation("Received request to get chatroom by id");

        if (string.IsNullOrWhiteSpace(id))
            return BadRequest($"{nameof(id)} is required");

        var chatroom = await _chatRoomService.Get(id);

        return chatroom is null ? NotFound() :
            GetChatRoomResponse.Convert(chatroom);
    }

    [HttpPost]
    public async Task<ActionResult> CreateChatRoom([FromBody] CreateChatRoomRequest request)
    {
        _logger.LogInformation("Received request to create chatroom");

        var command = CreateChatRoomRequest.Convert(request);
        var chatroom = await _chatRoomService.Create(command);
        return Ok(chatroom.Id);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateChatRoom([FromBody] UpdateChatRoomRequest request)
    {
        _logger.LogInformation("Received request to update chatroom");
        await Task.CompletedTask;
        return NoContent();
    }

    [HttpDelete("{chatroomId}")]
    public async Task<ActionResult> DeleteChatRoom(int chatroomId)
    {
        _logger.LogInformation("Received request to delete chatroom");
        await Task.CompletedTask;
        return NoContent();
    }

    [HttpGet("{chatroomId}/messages")]
    public async Task<ActionResult<GetChatMessagesResponse>> GetChatMessages(string chatroomId)
    {
        _logger.LogInformation("Received request to get chat messages by chatroomId");

        if (string.IsNullOrWhiteSpace(chatroomId))
            return BadRequest($"{nameof(chatroomId)} is required");

        var chatMessages = await _chatMessageService.GetByChatRoomId(chatroomId);

        //todo rod
        await Task.CompletedTask;
        return Ok();
    }

    [HttpPost("{chatroomId}/messages")]
    public async Task<ActionResult> CreateChatMessage(string chatroomId, [FromBody] CreateChatMessageRequest request)
    {
        _logger.LogInformation("Received request to create chat message");

        if (string.IsNullOrWhiteSpace(chatroomId))
            return BadRequest($"{nameof(chatroomId)} is required");

        //todo rod
        await Task.CompletedTask;
        return Ok();
    }

    //todo
    //chatrooms/{id}/users
    //get, add, update user name, delete
}