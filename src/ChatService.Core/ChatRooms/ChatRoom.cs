using ChatService.Core.Users;

namespace ChatService.Core.ChatRooms;

public class ChatRoom
{
    public string Id { get; init; }

    public string SuperUserId { get; init; }

    private readonly List<User> _users = [];

    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    public string Tracker { get; set; } = string.Empty;

    private ChatRoom(User superUser)
    {
        Id = Guid.NewGuid().ToString();
        SuperUserId = superUser.Id;
        _users = [superUser];
    }

    public static ChatRoom Load(User superUser)
    {
        return new ChatRoom(superUser);
    }

    public void AddUser(User user)
    {
        if (_users.Any(u => u.Id == user.Id))
            throw new InvalidOperationException("User already exists in the chat room");

        _users.Add(user);
    }
}
