using ChatService.Core.Users;
using Simply.Track;

namespace ChatService.Core.ChatRooms;

public class ChatRoom
{
    public string Id { get; init; }

    public string SuperUserId { get; init; }

    private List<User> _users = [];

    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    public Tracker Tracker { get; init; }

    private ChatRoom(User superUser, Tracker tracker)
    {
        Id = Guid.NewGuid().ToString();
        SuperUserId = superUser.Id;
        _users = [superUser];
        Tracker = tracker;
    }

    public static ChatRoom Load(User superUser, Tracker tracker) => new(superUser, tracker);

    public void AddUser(User user)
    {
        if (_users.Any(u => u.Id == user.Id))
            throw new InvalidOperationException("User already exists in the chat room");

        _users.Add(user);
    }
}