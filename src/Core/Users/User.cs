using Simply.Track;

namespace ChatService.Core.Users;

public class User
{
    public string Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public Tracker Tracker { get; init; }

    private User(string id, string name, Tracker tracker)
    {
        Id = id;
        Tracker = tracker;
        SetName(name);
    }

    public static User Load(string id, string name, Tracker tracker) => new(id, name, tracker);

    public void SetName(string name)
    {
        ValidateName(name);
        Name = name.Trim().ToLower();
    }

    public static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"{nameof(name)} is required");
        if (name.Length > 20)
            throw new ArgumentException($"{nameof(name)} is too long");
    }
}