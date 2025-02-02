using ChatService.Core.ChatRooms.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChatService.Core.Tests.Chatrooms.Models;

[TestClass]
public class ChatRoomUserTests
{
    [TestMethod]
    public void Create_ShouldReturnUserWithValidProperties()
    {
        var username = "testuser";

        var user = ChatRoomUser.Create(username);

        _ = user.Should().NotBeNull();
        _ = user.Id.Should().NotBeNullOrWhiteSpace();
        _ = user.Username.Should().Be(username);
        _ = user.Settings.Should().NotBeNull();
        _ = user.IsSuperUser.Should().BeFalse();
    }

    [TestMethod]
    public void CreateSuperUser_ShouldReturnUserWithSuperUserFlag()
    {
        var username = "testuser";

        var superUser = ChatRoomUser.CreateSuperUser(username);

        _ = superUser.Should().NotBeNull();
        _ = superUser.Id.Should().NotBeNullOrWhiteSpace();
        _ = superUser.Username.Should().Be(username);
        _ = superUser.Settings.Should().NotBeNull();

        _ = superUser.IsSuperUser.Should().BeTrue();
    }

    [TestMethod]
    public void Load_ShouldReturnUserWithSpecifiedProperties()
    {
        var id = "id";
        var username = "user";
        var settings = ChatRoomUserSettings.Create();
        var isSuperUser = true;

        var user = ChatRoomUser.Load(id, username, settings, isSuperUser);

        _ = user.Should().NotBeNull();
        _ = user.Id.Should().Be(id);
        _ = user.Username.Should().Be(username);
        _ = user.Settings.Should().Be(settings);
        _ = user.IsSuperUser.Should().Be(isSuperUser);
    }
}