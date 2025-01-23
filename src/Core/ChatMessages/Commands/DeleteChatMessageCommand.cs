namespace ChatService.Core.ChatMessages.Commands;
public class DeleteChatMessageCommand
{
    public string ChatMessageId { get; init; }
    private DeleteChatMessageCommand(string chatMessageId)
    {
        if (string.IsNullOrWhiteSpace(chatMessageId))
            throw new ArgumentException($"{nameof(chatMessageId)} is required");
        ChatMessageId = chatMessageId;
    }
    public static DeleteChatMessageCommand Create(string chatMessageId) => new(chatMessageId);
}