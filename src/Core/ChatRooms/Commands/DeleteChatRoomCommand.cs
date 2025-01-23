namespace ChatService.Core.Chatrooms.Commands;

public class DeleteChatroomCommand
{
    public string ChatroomId { get; init; }

    private DeleteChatroomCommand(string chatroomId)
    {
        if (string.IsNullOrWhiteSpace(chatroomId))
            throw new ArgumentException($"{nameof(chatroomId)} is required");

        ChatroomId = chatroomId;
    }

    public static DeleteChatroomCommand Create(string chatroomId) => new(chatroomId);
}