namespace ChatService.Core.ChatRooms.Models;

public abstract class ChatRoomBase
{
    private protected List<ChatRoomUser> _users = [];

    public IReadOnlyCollection<ChatRoomUser> Users => _users.AsReadOnly();

    public ChatRoomBase(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException($"{nameof(userId)} is required");

        var user = ChatRoomUser.CreateSuperUser(userId, ChatRoomUserSettings.Create());

        _users = [user];
    }

    public ChatRoomBase(List<ChatRoomUser> users) => _users = users.Count != 0 ?
        throw new ArgumentException("At least one user is required") :
        users;
}