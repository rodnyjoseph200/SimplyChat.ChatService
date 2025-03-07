using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;

namespace ChatService.Core.ChatMessages.Commands;
public record CreateChatMessageCommand : ChatMessageBase
{
    private CreateChatMessageCommand(ID chatRoomId, ID userId, string content, DateTimeOffset createdAt, ChatMessageTypes type) : base(chatRoomId, userId, content, createdAt, type)
    { }

    public static CreateChatMessageCommand Create(ID chatRoomId, ID userId, string content, DateTimeOffset createdAt, ChatMessageTypes type) =>
        new(chatRoomId, userId, content, createdAt, type);
}