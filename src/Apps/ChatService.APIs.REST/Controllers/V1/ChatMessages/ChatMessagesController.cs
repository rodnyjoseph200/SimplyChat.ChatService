using Asp.Versioning;
using ChatService.APIs.REST.Controllers.V1.ChatMessages.Models;
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

    public ChatMessagesController(ILogger<ChatMessagesController> logger, IChatMessageService chatMessageService)
    {
        _logger = logger;
        _chatMessageService = chatMessageService;
    }

    [HttpGet("{messageId}")]
    public async Task<ActionResult<GetChatMessageResponse>> Get(string messageId)
    {
        _logger.LogInformation("Received request to get chat message by messageId");
        var chatMessage = await _chatMessageService.Get(messageId);
        _logger.LogInformation("Request to get chat message by messageId completed");
        return chatMessage is null ? NotFound() :
            GetChatMessageResponse.Convert(chatMessage);
    }

    [HttpPut()]
    public async Task<ActionResult> Update([FromBody] UpdateChatMessageRequest request)
    {
        _logger.LogInformation("Received request to update chat message");
        var command = UpdateChatMessageRequest.Convert(request);
        await _chatMessageService.Update(command);
        _logger.LogInformation("Request to update chat message completed");
        return NoContent();
    }

    [HttpDelete()]
    public async Task<ActionResult> Delete([FromBody] DeleteChatMessageRequest request)
    {
        _logger.LogInformation("Received request to delete chat message");
        var command = DeleteChatMessageRequest.Convert(request);
        await _chatMessageService.Delete(command);
        _logger.LogInformation("Request to delete chat message completed");
        return NoContent();
    }
}