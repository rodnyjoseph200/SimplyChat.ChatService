namespace ChatService.Core.ChatRooms.Commands;

public record UpdateChatRoomCommand
{
    public string ChatRoomId { get; }

    private UpdateChatRoomCommand(string chatRoomId)
    {
        ChatRoomId = chatRoomId;
    }

    public static UpdateChatRoomCommand Create(string chatRoomId)
    {
        return new UpdateChatRoomCommand(chatRoomId);
    }
}