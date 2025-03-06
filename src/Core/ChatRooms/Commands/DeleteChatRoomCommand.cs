namespace ChatService.Core.ChatRooms.Commands;

public record DeleteChatRoomCommand
{
    public string ChatRoomId { get; }

    private DeleteChatRoomCommand(string chatroomId)
    {
        if (string.IsNullOrWhiteSpace(chatroomId))
            throw new ArgumentException($"{nameof(chatroomId)} is required");

        ChatRoomId = chatroomId;
    }

    public static DeleteChatRoomCommand Create(string chatroomId) => new(chatroomId);
}