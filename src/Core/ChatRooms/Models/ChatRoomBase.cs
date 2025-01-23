namespace ChatService.Core.Chatrooms.Models;

public abstract class ChatroomBase
{
    private protected List<ChatroomUser> _users = [];

    public IReadOnlyCollection<ChatroomUser> Users => _users.AsReadOnly();

    public ChatroomBase(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException($"{nameof(userId)} is required");

        var user = ChatroomUser.CreateSuperUser(userId, ChatroomUserSettings.Create());

        _users = [user];
    }

    public ChatroomBase(List<ChatroomUser> users) => _users = users.Count != 0 ?
        throw new ArgumentException("At least one user is required") :
        users;
}