using Simply.Track;

namespace ChatService.Core.Users;

public class User
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    public Tracker Tracker { get; init; }

    private User(string username, Tracker tracker)
    {
        Id = Guid.NewGuid().ToString();
        Tracker = tracker;
        SetName(username);
    }

    public static User Load(string username, Tracker tracker) => new(username, tracker);

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