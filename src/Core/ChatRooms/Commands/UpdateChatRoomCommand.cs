namespace ChatService.Core.Chatrooms.Commands;

public class UpdateChatroomCommand
{
    public string ChatroomId { get; init; }

    private UpdateChatroomCommand(string chatroomId)
    {
        ChatroomId = chatroomId;
    }

    public static UpdateChatroomCommand Create(string chatroomId)
    {
        return new UpdateChatroomCommand(chatroomId);
    }
}