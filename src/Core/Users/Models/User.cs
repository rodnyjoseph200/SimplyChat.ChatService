using Simply.Track;

namespace ChatService.Core.Users.Models;

public record User
{
    public string Id { get; }
    public string Name { get; private set; } = string.Empty;
    public Tracker Tracker { get; }

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