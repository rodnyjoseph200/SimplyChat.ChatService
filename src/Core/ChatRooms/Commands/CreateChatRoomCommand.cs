namespace ChatService.Core.ChatRooms.Commands;

public record CreateChatRoomCommand
{
    public string Username { get; }

    private CreateChatRoomCommand(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException($"{nameof(username)} is required");

        Username = username;
    }

    public static CreateChatRoomCommand Create(string username) => new(username);
}