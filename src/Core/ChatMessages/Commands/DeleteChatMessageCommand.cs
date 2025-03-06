namespace ChatService.Core.ChatMessages.Commands;
public record DeleteChatMessageCommand
{
    public string ChatroomId { get; }
    public string ChatMessageId { get; }

    private DeleteChatMessageCommand(string chatroomId, string chatMessageId)
    {
        if (string.IsNullOrWhiteSpace(chatroomId))
            throw new ArgumentException($"{nameof(chatroomId)} is required");
        if (string.IsNullOrWhiteSpace(chatMessageId))
            throw new ArgumentException($"{nameof(chatMessageId)} is required");

        ChatMessageId = chatMessageId;
        ChatroomId = chatroomId;
    }
    public static DeleteChatMessageCommand Create(string chatroomId, string chatMessageId) =>
        new(chatroomId, chatMessageId);
}