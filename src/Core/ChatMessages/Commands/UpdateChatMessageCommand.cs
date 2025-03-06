﻿namespace ChatService.Core.ChatMessages.Commands;
public record UpdateChatMessageCommand
{
    public string ChatroomId { get; }

    public string ChatMessageId { get; }

    public string Content { get; }

    private UpdateChatMessageCommand(string chatroomId, string chatMessageId, string content)
    {
        if (string.IsNullOrWhiteSpace(chatroomId))
            throw new ArgumentException($"{nameof(chatroomId)} is required");
        if (string.IsNullOrWhiteSpace(chatMessageId))
            throw new ArgumentException($"{nameof(chatMessageId)} is required");
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException($"{nameof(content)} is required");

        ChatroomId = chatroomId;
        ChatMessageId = chatMessageId;
        Content = content;
    }

    public static UpdateChatMessageCommand Create(string chatroomId, string chatMessageId, string content) =>
        new(chatroomId, chatMessageId, content);
}