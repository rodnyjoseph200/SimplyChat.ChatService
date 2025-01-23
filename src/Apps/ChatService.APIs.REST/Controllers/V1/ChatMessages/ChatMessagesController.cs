using Asp.Versioning;
using ChatService.APIs.REST.Controllers.V1.ChatRooms.Models.Messages;
using ChatService.Core.ChatMessages;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.APIs.REST.Controllers.V1.ChatMessages;

[ApiController]
[ApiVersion("1.0")]
[Route("api/chat-service/v{version:apiVersion}/messages")]
public class ChatMessagesController : ControllerBase
{
    private readonly ILogger<ChatMessagesController> _logger;
    private readonly IChatMessageService _chatMessageService;

    //todo - remove IChatMessageService
    public ChatMessagesController(ILogger<ChatMessagesController> logger, IChatMessageService messageService)
    {
        _logger = logger;
        _chatMessageService = messageService;
    }

    [HttpGet("{messageId}")]
    public async Task<ActionResult<GetChatMessageResponse>> GetChatMessage(string messageId)
    {
        _logger.LogInformation("Received request to get chat message by messageId");

        if (string.IsNullOrWhiteSpace(messageId))
            return BadRequest($"{nameof(messageId)} is required");

        var chatMessage = await _chatMessageService.Get(messageId);

        //todo rod
        //return chatMessage is null ? NotFound() :
        //    GetChatMessageResponse.Convert(chatMessage);

        await Task.CompletedTask;
        return Ok();
    }

    [HttpPut("messages")]
    public async Task<ActionResult> UpdateChatMessage([FromBody] UpdateChatMessageRequest request)
    {
        _logger.LogInformation("Received request to update chat message");

        await Task.CompletedTask;
        return NoContent();
    }

    [HttpDelete("messages")]
    public async Task<ActionResult> DeleteChatMessage([FromBody] DeleteChatMessageRequest request)
    {
        _logger.LogInformation("Received request to delete chat message");

        await Task.CompletedTask;
        return NoContent();
    }
}
