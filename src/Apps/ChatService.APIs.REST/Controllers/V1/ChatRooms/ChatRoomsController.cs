using Asp.Versioning;
using ChatService.APIs.REST.Controllers.V1.Chatrooms.Models;
using ChatService.APIs.REST.Controllers.V1.Chatrooms.Models.Messages;
using ChatService.Core.ChatMessages;
using ChatService.Core.Chatrooms;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.APIs.REST.Controllers.V1.Chatrooms;

[ApiController]
[ApiVersion("1.0")]
[Route("api/chat-service/v{version:apiVersion}/[controller]")]
public class ChatroomsController : ControllerBase
{
    private readonly ILogger<ChatroomsController> _logger;
    private readonly IChatroomService _chatroomService;
    private readonly IChatMessageService _chatMessageService;

    public ChatroomsController(ILogger<ChatroomsController> logger, IChatroomService chatroomService, IChatMessageService messageService)
    {
        _logger = logger;
        _chatroomService = chatroomService;
        _chatMessageService = messageService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetChatroomResponse>> GetChatroom(string id)
    {
        _logger.LogInformation("Received request to get chatroom by id");

        if (string.IsNullOrWhiteSpace(id))
            return BadRequest($"{nameof(id)} is required");

        var chatroom = await _chatroomService.Get(id);

        return chatroom is null ? NotFound() :
            GetChatroomResponse.Convert(chatroom);
    }

    [HttpPost]
    public async Task<ActionResult> CreateChatroom([FromBody] CreateChatroomRequest request)
    {
        _logger.LogInformation("Received request to create chatroom");

        var command = CreateChatroomRequest.Convert(request);
        var chatroom = await _chatroomService.Create(command);
        return Ok(chatroom.Id);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateChatroom([FromBody] UpdateChatroomRequest request)
    {
        _logger.LogInformation("Received request to update chatroom");
        await Task.CompletedTask;
        return NoContent();
    }

    [HttpDelete("{chatroomId}")]
    public async Task<ActionResult> DeleteChatroom(int chatroomId)
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

        var chatMessages = await _chatMessageService.GetByChatroomId(chatroomId);

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