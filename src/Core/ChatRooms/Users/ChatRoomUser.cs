namespace ChatService.Core.ChatRooms.Users;

public class ChatRoomUser
{
    public string Id { get; init; }
    public string Name { get; private set; } = string.Empty;

    private ChatRoomUser(string username)
    {
        // todo - don't set guid
        Id = Guid.NewGuid().ToString();
        SetName(username);
    }

    // fix
    public static ChatRoomUser Load(string username) => new(username);

    public void SetName(string username)
    {
        ValidateName(username);
        Name = username.Trim().ToLower();
    }

    public static void ValidateName(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException($"{nameof(username)} is required");
        if (username.Length > 20)
            throw new ArgumentException($"{nameof(username)} is too long");
    }
}