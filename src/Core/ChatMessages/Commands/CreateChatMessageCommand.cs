using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;

namespace ChatService.Core.ChatMessages.Commands;
public class CreateChatMessageCommand : ChatMessageBase
{
    private CreateChatMessageCommand(string chatroomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type) : base(chatroomId, userId, content, createdAt, type)
    {
    }

    public static CreateChatMessageCommand Create(string chatroomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
    {
        return new CreateChatMessageCommand(chatroomId, userId, content, createdAt, type);
    }
}