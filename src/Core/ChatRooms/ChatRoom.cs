using ChatService.Core.ChatRooms.Users;
using Simply.Track;

namespace ChatService.Core.ChatRooms;

//Todo update and fix this model
public class ChatRoom
{
    public Tracker Tracker { get; init; }

    public string Id { get; init; }

    public string SuperUserId { get; init; }

    private readonly List<ChatRoomUser> _users = [];

    public IReadOnlyCollection<ChatRoomUser> Users => _users.AsReadOnly();

    private ChatRoom(ChatRoomUser superUser, Tracker tracker)
    {
        // todo - don't set guid
        Id = Guid.NewGuid().ToString();
        SuperUserId = superUser.Id;
        _users = [superUser];
        Tracker = tracker;
    }

    //fix
    public static ChatRoom Load(ChatRoomUser superUser, Tracker tracker) => new(superUser, tracker);

    public void AddUser(ChatRoomUser user)
    {
        if (_users.Any(u => u.Id == user.Id))
            throw new InvalidOperationException("User already exists in the chat room");

        _users.Add(user);
    }
}