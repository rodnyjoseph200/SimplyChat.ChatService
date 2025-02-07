using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChatService.Core.Tests.ChatMessages.models;

[TestClass]
public class NewChatMessageTests
{
    [TestMethod]
    public void Create_WithValidParameters_ShouldReturnNewChatMessage()
    {
        var chatRoomId = "room1";
        var userId = "user1";
        var content = "Hello, world!";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        var newChatMessage = NewChatMessage.Create(chatRoomId, userId, content, createdAt, type);

        _ = newChatMessage.Should().NotBeNull();
        _ = newChatMessage.ChatroomId.Should().Be(chatRoomId);
        _ = newChatMessage.UserId.Should().Be(userId);
        _ = newChatMessage.Content.Should().Be(content);
        _ = newChatMessage.CreatedAt.Should().Be(createdAt);
        _ = newChatMessage.Type.Should().Be(type);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void Create_WithInvalidChatRoomId_ShouldThrowArgumentException(string invalidChatRoomId)
    {
        var userId = "user1";
        var content = "Hello";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        Action act = () => NewChatMessage.Create(invalidChatRoomId, userId, content, createdAt, type);

        _ = act.Should().Throw<ArgumentException>();
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void Create_WithInvalidUserId_ShouldThrowArgumentException(string invalidUserId)
    {
        var chatRoomId = "room1";
        var content = "Hello";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        Action act = () => NewChatMessage.Create(chatRoomId, invalidUserId, content, createdAt, type);

        _ = act.Should().Throw<ArgumentException>();
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public void Create_WithInvalidContent_ShouldThrowArgumentException(string invalidContent)
    {
        var chatRoomId = "room1";
        var userId = "user1";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        Action act = () => NewChatMessage.Create(chatRoomId, userId, invalidContent, createdAt, type);

        _ = act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void Create_WithDefaultCreatedAt_ShouldThrowArgumentException()
    {
        var chatRoomId = "room1";
        var userId = "user1";
        var content = "Hello";
        DateTimeOffset createdAt = default;
        var type = ChatMessageTypes.Text;

        Action act = () => NewChatMessage.Create(chatRoomId, userId, content, createdAt, type);

        _ = act.Should().Throw<ArgumentException>()
           .WithMessage("createdAt is required.");
    }
}