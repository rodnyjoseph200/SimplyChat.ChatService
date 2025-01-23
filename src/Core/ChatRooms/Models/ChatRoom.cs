using Simply.Track;

namespace ChatService.Core.ChatRooms.Models;

public class Chatroom : ChatRoomBase
{
    public string Id { get; init; }

    public Tracker Tracker { get; init; }

    private Chatroom(string id, List<ChatRoomUser> users, Tracker tracker) : base(users)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException($"{nameof(id)} is required");

        Id = id;
        Tracker = tracker;
    }

    public static Chatroom Load(string id, List<ChatRoomUser> users, Tracker tracker) => new(id, users, tracker);

    public void AddUser(ChatRoomUser user)
    {
        if (_users.Any(u => u.Id == user.Id))
            throw new InvalidOperationException($"{nameof(user)} {user.Id} already exists in the chat room");

        _users.Add(user);
    }

    public void RemoveUser(string userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId) ??
            throw new InvalidOperationException($"{nameof(userId)} {userId} does not exist in the chat room");

        _ = _users.Remove(user);
    }
}