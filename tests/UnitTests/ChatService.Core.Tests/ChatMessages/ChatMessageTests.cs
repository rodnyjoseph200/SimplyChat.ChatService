using ChatService.Core.ChatMessages.Models;
using ChatService.Core.Messages;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simply.Track;

namespace ChatService.Core.Tests.ChatMessages;

[TestClass]
public class ChatMessageTests
{
    [TestMethod]
    public void Load_WithValidParameters_ShouldReturnChatMessage()
    {
        var id = "msg1";
        var chatRoomId = "room1";
        var userId = "user1";
        var content = "Hello";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        var tracker = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "creator",
            DateTimeOffset.UtcNow, "updater",
            false, null, null, null, null);

        var chatMessage = ChatMessage.Load(tracker, id, chatRoomId, userId, content, createdAt, type);

        _ = chatMessage.Should().NotBeNull();
        _ = chatMessage.Id.Should().Be(id);
        _ = chatMessage.Tracker.Should().Be(tracker);
        _ = chatMessage.ChatroomId.Should().Be(chatRoomId);
        _ = chatMessage.UserId.Should().Be(userId);
        _ = chatMessage.Content.Should().Be(content);
        _ = chatMessage.CreatedAt.Should().Be(createdAt);
        _ = chatMessage.Type.Should().Be(type);
    }

    [TestMethod]
    public void UpdateContent_ShouldUpdateContentProperty()
    {
        var id = "msg1";
        var chatRoomId = "room1";
        var userId = "user1";
        var initialContent = "Hello";
        DateTimeOffset createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        var tracker = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "creator",
            DateTimeOffset.UtcNow, "updater",
            false, null, null, null, null);
        var chatMessage = ChatMessage.Load(tracker, id, chatRoomId, userId, initialContent, createdAt, type);

        var newContent = "Updated content";
        chatMessage.UpdateContent(newContent);

        _ = chatMessage.Content.Should().Be(newContent);
    }
}