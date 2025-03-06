using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChatService.Core.Tests.ChatMessages.models;

public record DummyChatMessage : ChatMessageBase
{
    public DummyChatMessage(string chatRoomId, string userId, string content, DateTimeOffset createdAt, ChatMessageTypes type)
        : base(chatRoomId, userId, content, createdAt, type)
    {
    }
}

[TestClass]
public class ChatMessageBaseTests
{
    [TestMethod]
    public void Constructor_WithValidParameters_ShouldAssignProperties()
    {
        var chatRoomId = "room1";
        var userId = "user1";
        var content = "Hello";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        var dummy = new DummyChatMessage(chatRoomId, userId, content, createdAt, type);

        _ = dummy.ChatroomId.Should().Be(chatRoomId);
        _ = dummy.UserId.Should().Be(userId);
        _ = dummy.Content.Should().Be(content);
        _ = dummy.CreatedAt.Should().Be(createdAt);
        _ = dummy.Type.Should().Be(type);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("  ")]
    public void Constructor_WithInvalidChatRoomId_ShouldThrowArgumentException(string invalidChatRoomId)
    {
        var userId = "user1";
        var content = "Hello";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        Action act = () => new DummyChatMessage(invalidChatRoomId, userId, content, createdAt, type);

        // 
        _ = act.Should().Throw<ArgumentException>();
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("  ")]
    public void Constructor_WithInvalidUserId_ShouldThrowArgumentException(string invalidUserId)
    {
        var chatRoomId = "room1";
        var content = "Hello";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        Action act = () => new DummyChatMessage(chatRoomId, invalidUserId, content, createdAt, type);

        _ = act.Should().Throw<ArgumentException>();
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("  ")]
    public void Constructor_WithInvalidContent_ShouldThrowArgumentException(string invalidContent)
    {
        var chatRoomId = "room1";
        var userId = "user1";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        Action act = () => new DummyChatMessage(chatRoomId, userId, invalidContent, createdAt, type);

        _ = act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void Constructor_WithDefaultCreatedAt_ShouldThrowArgumentException()
    {
        var chatRoomId = "room1";
        var userId = "user1";
        var content = "Hello";
        DateTimeOffset createdAt = default;
        var type = ChatMessageTypes.Text;

        Action act = () => new DummyChatMessage(chatRoomId, userId, content, createdAt, type);

        _ = act.Should().Throw<ArgumentException>()
            .WithMessage("createdAt is required.");
    }
}