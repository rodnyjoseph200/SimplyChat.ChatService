using Asp.Versioning;
using ChatService.APIs.REST.Controllers.V1.ChatMessages.Models;
using ChatService.APIs.REST.Controllers.V1.ChatRooms.Models.Messages;
using ChatService.Core.ChatMessages;
using ChatService.Core.ChatMessages.Commands;
using Microsoft.AspNetCore.Mvc;
using Simply.Log;
using Swashbuckle.AspNetCore.Annotations;

namespace ChatService.APIs.REST.Controllers.V1.ChatMessages;

[ApiController]
[ApiVersion("1.0")]
[Route("api/chat-service/v{version:apiVersion}")]
public class ChatMessagesController : ControllerBase
{
    private readonly ILogger<ChatMessagesController> _logger;
    private readonly IChatMessageService _chatMessageService;

    public ChatMessagesController(ILogger<ChatMessagesController> logger, IChatMessageService chatMessageService)
    {
        _logger = logger;
        _chatMessageService = chatMessageService;
    }

    [HttpGet("chatrooms/{chatroomId}/messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Get Chat Messages by Chatroom ID", Description = "Gets Chat Messages by Chatroom ID")]
    public async Task<ActionResult<GetChatMessagesByChatroomIdResponse>> Get(string chatroomId)
    {
        using var _ = _logger.AddField($"{nameof(chatroomId)}", chatroomId);

        _logger.LogInformation("Received request to get chat messages by chatroomId");

        var chatMessages = await _chatMessageService.GetByChatRoomId(chatroomId);
        var response = GetChatMessagesByChatroomIdResponse.Convert(chatroomId, chatMessages);
        return Ok(response);
    }

    [HttpGet("chatrooms/{chatroomId}/messages/{messageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Get Chat Message by ID", Description = "Gets a Chat Message by its ID")]
    public async Task<ActionResult<GetChatMessageResponse>> Get(string chatroomId, string messageId)
    {
        using var _ = _logger.AddFields(
            ($"{nameof(chatroomId)}", chatroomId),
            ($"{nameof(messageId)}", messageId));

        _logger.LogInformation("Received request to get chat message by messageId");

        var chatMessage = await _chatMessageService.Get(chatroomId, messageId);

        _logger.LogInformation("Request to get chat message by messageId completed");
        return chatMessage is null ? NotFound() :
            GetChatMessageResponse.Convert(chatMessage);
    }

    [HttpPost("chatrooms/{chatroomId}/messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Create Chat Message", Description = "Creates Chat Message")]
    public async Task<ActionResult<CreateChatMessageResponse>> CreateChatMessage(string chatroomId, [FromBody] CreateChatMessageRequest request)
    {
        using var _ = _logger.AddField($"{nameof(chatroomId)}", chatroomId);

        _logger.LogInformation("Received request to create chat message provided chatroomId");

        var command = CreateChatMessageRequest.Convert(chatroomId, request);

        using var __ = _logger.AddFields(
            ($"{nameof(command.UserId)}", command.UserId),
            ($"{nameof(command.CreatedAt)}", command.CreatedAt.ToString()),
            ($"{nameof(command.Type)}", command.Type.ToString()));

        var chatMessage = await _chatMessageService.Create(command);
        var response = CreateChatMessageResponse.Convert(chatMessage);
        _logger.LogInformation("Request to create chat messages provided chatroomId completed");
        return Ok(response);
    }

    [HttpPut("chatrooms/{chatroomId}/messages/{messageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Update Chat Message", Description = "Updates a Chat Message")]
    public async Task<ActionResult> Update(string chatroomId, string messageId, [FromBody] UpdateChatMessageRequest request)
    {
        using var _ = _logger.AddFields(
            ($"{nameof(chatroomId)}", chatroomId),
            ($"{nameof(messageId)}", messageId));

        _logger.LogInformation("Received request to update chat message");

        var command = UpdateChatMessageRequest.Convert(chatroomId, messageId, request);
        await _chatMessageService.Update(command);

        _logger.LogInformation("Request to update chat message completed");
        return NoContent();
    }

    [HttpDelete("chatrooms/{chatroomId}/messages/{messageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Delete Chat Message", Description = "Deletes a Chat Message")]
    public async Task<ActionResult> Delete(string chatroomId, string messageId)
    {
        using var _ = _logger.AddFields(
            ($"{nameof(chatroomId)}", chatroomId),
            ($"{nameof(messageId)}", messageId));

        _logger.LogInformation("Received request to delete chat message");

        var command = DeleteChatMessageCommand.Create(chatroomId, messageId);
        await _chatMessageService.Delete(command);

        _logger.LogInformation("Request to delete chat message completed");
        return NoContent();
    }
}