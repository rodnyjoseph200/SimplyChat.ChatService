using ChatService.Core.Chatrooms.Models.Users;
using ChatService.Core.ChatRooms.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChatService.Core.Tests.Chatrooms.Models;

public record DummyChatRoom : ChatRoomBase
{
    public DummyChatRoom(ChatRoomUser user)
        : base(user)
    {
    }

    public DummyChatRoom(List<ChatRoomUser> users)
        : base(users)
    {
    }
}

[TestClass]
public class ChatRoomBaseTests
{
    [TestMethod]
    public void Constructor_WithSingleUser_ShouldInitializeUsers()
    {
        var user = ChatRoomUser.Create("user");

        var dummy = new DummyChatRoom(user);

        _ = dummy.Users.Should().ContainSingle().Which.Should().Be(user);
    }

    [TestMethod]
    public void Constructor_WithUserList_ShouldInitializeUsers()
    {
        var user1 = ChatRoomUser.Create("user1");
        var user2 = ChatRoomUser.Create("user2");
        var users = new List<ChatRoomUser> { user1, user2 };

        var dummy = new DummyChatRoom(users);

        _ = dummy.Users.Should().BeEquivalentTo(users);
    }

    [TestMethod]
    public void Constructor_WithEmptyUserList_ShouldThrowArgumentException()
    {
        var emptyUsers = new List<ChatRoomUser>();

        Action act = () => new DummyChatRoom(emptyUsers);

        _ = act.Should().Throw<ArgumentException>()
           .WithMessage("At least one user is required");
    }
}