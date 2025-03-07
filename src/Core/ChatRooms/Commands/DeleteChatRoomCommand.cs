namespace ChatService.Core.ChatRooms.Commands;

public record DeleteChatRoomCommand
{
    public ID ChatRoomId { get; }

    private DeleteChatRoomCommand(ID chatroomId)
    {
        ChatRoomId = chatroomId;
    }

    public static DeleteChatRoomCommand Create(ID chatroomId) => new(chatroomId);
}