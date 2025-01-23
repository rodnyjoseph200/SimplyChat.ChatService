namespace ChatService.Core.ChatMessages.Commands;
public class UpdateChatMessageCommand
{
    public string ChatMessageId { get; init; }

    public string Content { get; init; }

    private UpdateChatMessageCommand(string chatMessageId, string content)
    {
        if (string.IsNullOrWhiteSpace(chatMessageId))
            throw new ArgumentException($"{nameof(chatMessageId)} is required");
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException($"{nameof(content)} is required");
        ChatMessageId = chatMessageId;
        Content = content;
    }

    public static UpdateChatMessageCommand Create(string chatMessageId, string content) => new(chatMessageId, content);
}