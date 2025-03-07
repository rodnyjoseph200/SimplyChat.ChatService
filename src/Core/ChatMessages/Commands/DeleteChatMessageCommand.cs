namespace ChatService.Core.ChatMessages.Commands;
public record DeleteChatMessageCommand
{
    public ID ChatroomId { get; }
    public ID ChatMessageId { get; }

    private DeleteChatMessageCommand(ID chatroomId, ID chatMessageId)
    {
        ChatMessageId = chatMessageId;
        ChatroomId = chatroomId;
    }

    public static DeleteChatMessageCommand Create(ID chatroomId, ID chatMessageId) =>
        new(chatroomId, chatMessageId);
}