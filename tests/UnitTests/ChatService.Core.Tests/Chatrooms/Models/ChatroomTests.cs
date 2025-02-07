using ChatService.Core.ChatRooms.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simply.Track;

namespace ChatService.Core.Tests.Chatrooms.Models;

[TestClass]
public class ChatroomTests
{
    // Helper to create a dummy tracker.
    private Tracker CreateDummyTracker() => Tracker.Load(
        DateTimeOffset.UtcNow, "creator",
        DateTimeOffset.UtcNow, "updater",
        false, null, null, null, null);

    [TestMethod]
    public void Load_WithValidParameters_ShouldCreateChatroom()
    {
        var id = "room1";
        var tracker = CreateDummyTracker();

        var superUser = ChatRoomUser.CreateSuperUser("super");
        var users = new List<ChatRoomUser> { superUser };

        var chatroom = Chatroom.Load(id, users, tracker);

        _ = chatroom.Should().NotBeNull();
        _ = chatroom.Id.Should().Be(id);
        _ = chatroom.Tracker.Should().Be(tracker);
        _ = chatroom.Users.Should().BeEquivalentTo(users);
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void Load_WithInvalidId_ShouldThrowArgumentException(string invalidId)
    {
        var tracker = CreateDummyTracker();
        var superUser = ChatRoomUser.CreateSuperUser("super");
        var users = new List<ChatRoomUser> { superUser };

        Action act = () => Chatroom.Load(invalidId, users, tracker);

        _ = act.Should().Throw<ArgumentException>()
           .WithMessage("id is required");
    }

    [TestMethod]
    public void SuperUser_WhenExists_ShouldReturnSuperUser()
    {
        var id = "room1";
        var tracker = CreateDummyTracker();
        var superUser = ChatRoomUser.CreateSuperUser("super");
        var normalUser = ChatRoomUser.Create("user");

        // Order does not matter because SuperUser uses SingleOrDefault.
        var users = new List<ChatRoomUser> { normalUser, superUser };
        var chatroom = Chatroom.Load(id, users, tracker);

        var result = chatroom.SuperUser;

        _ = result.Should().Be(superUser);
    }

    [TestMethod]
    public void SuperUser_WhenNotExists_ShouldThrowInvalidOperationException()
    {
        var id = "room1";
        var tracker = CreateDummyTracker();

        var normalUser = ChatRoomUser.Create("user");
        var users = new List<ChatRoomUser> { normalUser };
        var chatroom = Chatroom.Load(id, users, tracker);

        Action act = () => { _ = chatroom.SuperUser; };

        _ = act.Should().Throw<InvalidOperationException>()
           .WithMessage("Super user does not exist in the chat room");
    }

    [TestMethod]
    public void AddUser_WithNewUser_ShouldIncreaseUserCount()
    {
        var id = "room1";
        var tracker = CreateDummyTracker();

        var superUser = ChatRoomUser.CreateSuperUser("super");
        var chatroom = Chatroom.Load(id, new List<ChatRoomUser> { superUser }, tracker);
        var newUser = ChatRoomUser.Create("newUser");

        chatroom.AddUser(newUser);

        _ = chatroom.Users.Should().Contain(newUser);
        _ = chatroom.Users.Count.Should().Be(2);
    }

    [TestMethod]
    public void AddUser_WithExistingUser_ShouldThrowInvalidOperationException()
    {
        var id = "room1";
        var tracker = CreateDummyTracker();

        var superUser = ChatRoomUser.CreateSuperUser("super");
        var chatroom = Chatroom.Load(id, new List<ChatRoomUser> { superUser }, tracker);

        // Create a duplicate user with the same Id as superUser.
        var duplicateUser = ChatRoomUser.Load(superUser.Id, "super", superUser.Settings, superUser.IsSuperUser);

        Action act = () => chatroom.AddUser(duplicateUser);

        _ = act.Should().Throw<InvalidOperationException>()
           .WithMessage($"user {superUser.Id} already exists in the chat room");
    }

    [TestMethod]
    public void RemoveUser_WithExistingUser_ShouldDecreaseUserCount()
    {
        var id = "room1";
        var tracker = CreateDummyTracker();

        var superUser = ChatRoomUser.CreateSuperUser("super");
        var normalUser = ChatRoomUser.Create("user");
        var users = new List<ChatRoomUser> { superUser, normalUser };
        var chatroom = Chatroom.Load(id, users, tracker);

        chatroom.RemoveUser(normalUser.Id);

        _ = chatroom.Users.Should().NotContain(u => u.Id == normalUser.Id);
        _ = chatroom.Users.Count.Should().Be(1);
    }

    [TestMethod]
    public void RemoveUser_WithNonExistingUser_ShouldThrowInvalidOperationException()
    {
        var id = "room1";
        var tracker = CreateDummyTracker();

        var superUser = ChatRoomUser.CreateSuperUser("super");
        var chatroom = Chatroom.Load(id, new List<ChatRoomUser> { superUser }, tracker);
        var nonExistentUserId = "nonexistent";

        Action act = () => chatroom.RemoveUser(nonExistentUserId);

        _ = act.Should().Throw<InvalidOperationException>()
           .WithMessage($"userId {nonExistentUserId} does not exist in the chat room");
    }
}