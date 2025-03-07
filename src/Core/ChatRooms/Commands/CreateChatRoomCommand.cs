using Guarded.Guards;

namespace ChatService.Core.ChatRooms.Commands;

public record CreateChatRoomCommand
{
    public string Username { get; }

    private CreateChatRoomCommand(string username)
    {
        _ = Guard.AgainstNullsAndWhitespaces(username);

        Username = username;
    }

    public static CreateChatRoomCommand Create(string username) => new(username);
}