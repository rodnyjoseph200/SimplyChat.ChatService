using ChatService.Core.Chatrooms.Models.Users;
using Simply.Track;

namespace ChatService.Core.ChatRooms.Models;

public record Chatroom : ChatRoomBase
{
    public ID Id { get; }

    public Tracker Tracker { get; }

    public ChatRoomUser SuperUser => _users.SingleOrDefault(u => u.IsSuperUser) ??
        throw new InvalidOperationException("Super user does not exist in the chat room");

    private Chatroom(ID id, List<ChatRoomUser> users, Tracker tracker) : base(users)
    {
        Id = id;
        Tracker = tracker;
    }

    public static Chatroom Load(ID id, List<ChatRoomUser> users, Tracker tracker) => new(id, users, tracker);

    public void AddUser(ChatRoomUser user)
    {
        if (_users.Any(u => u.Id == user.Id))
            throw new InvalidOperationException($"{nameof(user)} {user.Id} already exists in the chat room");

        _users.Add(user);
    }

    public void RemoveUser(ID userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId) ??
            throw new InvalidOperationException($"{nameof(userId)} {userId} does not exist in the chat room");

        _ = _users.Remove(user);
    }
}