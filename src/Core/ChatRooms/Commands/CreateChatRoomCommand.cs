namespace ChatService.Core.Chatrooms.Commands;

public class CreateChatroomCommand
{
    public string Username { get; init; }

    private CreateChatroomCommand(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException($"{nameof(username)} is required");

        Username = username;
    }

    public static CreateChatroomCommand Create(string username) => new(username);
}