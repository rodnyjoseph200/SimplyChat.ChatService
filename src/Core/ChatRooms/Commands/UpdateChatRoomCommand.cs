namespace ChatService.Core.ChatRooms.Commands;

public record UpdateChatRoomCommand
{
    public ID ChatRoomId { get; }

    private UpdateChatRoomCommand(ID chatRoomId)
    {
        ChatRoomId = chatRoomId;
    }

    public static UpdateChatRoomCommand Create(ID chatRoomId)
    {
        return new UpdateChatRoomCommand(chatRoomId);
    }
}