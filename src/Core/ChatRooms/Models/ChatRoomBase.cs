using ChatService.Core.Chatrooms.Models.Users;

namespace ChatService.Core.ChatRooms.Models;

public abstract class ChatRoomBase
{
    private protected List<ChatRoomUser> _users = [];

    public IReadOnlyCollection<ChatRoomUser> Users => _users.AsReadOnly();

    protected ChatRoomBase(ChatRoomUser chatRoomUser)
    {
        _users = [chatRoomUser];
    }

    protected ChatRoomBase(List<ChatRoomUser> users) => _users = users.Count is 0 ?
        throw new ArgumentException("At least one user is required") :
        users;
}