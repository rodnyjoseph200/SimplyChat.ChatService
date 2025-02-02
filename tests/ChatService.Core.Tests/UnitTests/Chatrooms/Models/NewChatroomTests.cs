using ChatService.Core.ChatRooms.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChatService.Core.Tests.Chatrooms.Models;

[TestClass]
public class NewChatroomTests
{
    [TestMethod]
    public void Create_ShouldReturnNewChatroomWithInitialUser()
    {
        var user = ChatRoomUser.Create("user");

        var newChatroom = NewChatroom.Create(user);

        _ = newChatroom.Should().NotBeNull();
        _ = newChatroom.Users.Should().ContainSingle().Which.Should().Be(user);
    }
}