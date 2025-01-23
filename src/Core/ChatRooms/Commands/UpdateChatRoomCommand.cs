namespace ChatService.Core.ChatRooms.Commands;

public class UpdateChatRoomCommand
{
    public string ChatRoomId { get; init; }

    private UpdateChatRoomCommand(string chatRoomId)
    {
        ChatRoomId = chatRoomId;
    }

    public static UpdateChatRoomCommand Create(string chatRoomId)
    {
        return new UpdateChatRoomCommand(chatRoomId);
    }
}