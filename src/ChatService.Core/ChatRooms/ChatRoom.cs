using ChatService.Core.Users;

namespace ChatService.Core.ChatRooms;

public class ChatRoom
{
    public string Id { get; init; }

    public string SuperUserId { get; init; }

    private readonly List<User> _users = [];

    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    private ChatRoom(User superUser)
    {
        Id = Guid.NewGuid().ToString();
        SuperUserId = superUser.Id;
        _users = [superUser];
    }

    public static ChatRoom Create(User superUser)
    {
        return new ChatRoom(superUser);
    }
}
