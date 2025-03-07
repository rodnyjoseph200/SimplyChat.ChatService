using Guarded.Guards;

namespace ChatService.Core.ChatMessages.Commands;
public record UpdateChatMessageCommand
{
    public ID ChatroomId { get; }

    public ID ChatMessageId { get; }

    public string Content { get; }

    private UpdateChatMessageCommand(ID chatroomId, ID chatMessageId, string content)
    {
        _ = Guard.AgainstNullsAndWhitespaces(content);

        ChatroomId = chatroomId;
        ChatMessageId = chatMessageId;
        Content = content;
    }

    public static UpdateChatMessageCommand Create(ID chatroomId, ID chatMessageId, string content) =>
        new(chatroomId, chatMessageId, content);
}