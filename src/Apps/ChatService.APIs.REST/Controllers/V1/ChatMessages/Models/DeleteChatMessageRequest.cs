﻿using ChatService.Core.ChatMessages.Commands;

namespace ChatService.APIs.REST.Controllers.V1.ChatMessages.Models;

public class DeleteChatMessageRequest
{
    public required string ChatMessageId { get; set; }

    internal static DeleteChatMessageCommand Convert(DeleteChatMessageRequest request) =>
        DeleteChatMessageCommand.Create(request.ChatMessageId);
}