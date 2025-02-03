using ChatService.Core.ChatMessages;
using ChatService.Core.ChatMessages.Commands;
using ChatService.Core.ChatMessages.Models;
using ChatService.Core.ChatRooms;
using ChatService.Core.ChatRooms.Models;
using ChatService.Core.Exceptions;
using ChatService.Core.Messages;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simply.Track;

namespace ChatService.Core.Tests.ChatMessages;

public abstract class ChatMessageServiceTestBase
{
    protected Mock<ILogger<ChatRoomService>> _loggerMock = null!;
    protected Mock<IChatMessageRepository> _chatMessageRepositoryMock = null!;
    protected Mock<IChatRoomService> _chatRoomServiceMock = null!;
    protected ChatMessageService _chatMessageService = null!;

    [TestInitialize]
    public void Initialize()
    {
        _loggerMock = new Mock<ILogger<ChatRoomService>>();
        _chatMessageRepositoryMock = new Mock<IChatMessageRepository>();
        _chatRoomServiceMock = new Mock<IChatRoomService>();
        _chatMessageService = new ChatMessageService(
            _loggerMock.Object,
            _chatMessageRepositoryMock.Object,
            _chatRoomServiceMock.Object);
    }
}

[TestClass]
public class ChatMessageService_Get_Tests : ChatMessageServiceTestBase
{
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public async Task Get_WhenChatMessageIdIsEmptyOrWhitespace_ThrowsArgumentException(string chatMessageId)
    {
        Func<Task> act = async () => await _chatMessageService.Get(chatMessageId);

        _ = await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("id is required");
    }

    [TestMethod]
    public async Task Get_WhenChatMessageNotFound_ReturnsNull()
    {
        var chatMessageId = "chatMessageId";
        _ = _chatMessageRepositoryMock
            .Setup(x => x.Get(chatMessageId))
            .ReturnsAsync((ChatMessage?)null);

        var result = await _chatMessageService.Get(chatMessageId);

        _ = result.Should().BeNull();
    }

    [TestMethod]
    public async Task Get_WhenChatMessageFound_ReturnsChatMessage()
    {
        var chatMessageId = "messageId";
        var chatRoomId = "chatroomId";
        var userId = "userId";
        var content = "some content";
        var createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        var tracker = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "createdBy",
            DateTimeOffset.UtcNow, "updatedBy",
            false);

        var chatMessage = ChatMessage.Load(tracker, chatMessageId, chatRoomId, userId, content, createdAt, type);

        _ = _chatMessageRepositoryMock
            .Setup(x => x.Get(chatMessageId))
            .ReturnsAsync(chatMessage);

        var result = await _chatMessageService.Get(chatMessageId);

        _ = result.Should().Be(chatMessage);
    }
}

[TestClass]
public class ChatMessageService_GetByChatRoomId_Tests : ChatMessageServiceTestBase
{
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public async Task GetByChatRoomId_WhenChatroomIdIsEmptyOrWhitespace_ThrowsArgumentException(string chatroomId)
    {
        Func<Task> act = async () => await _chatMessageService.GetByChatRoomId(chatroomId);

        _ = await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("chatroomId is required");
    }

    [TestMethod]
    public async Task GetByChatRoomId_WhenChatroomNotFound_ThrowsBadRequestException()
    {
        var chatroomId = "nonexistentChatroom";
        _ = _chatRoomServiceMock
            .Setup(x => x.Get(chatroomId))
            .ReturnsAsync((Chatroom?)null);

        Func<Task> act = async () => await _chatMessageService.GetByChatRoomId(chatroomId);

        _ = await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Chatroom not found");
    }

    [TestMethod]
    public async Task GetByChatRoomId_WhenNoChatMessagesFound_ReturnsEmptyCollection()
    {
        var chatroomId = "chatroomId";

        var trackerRoom = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "createdBy",
            DateTimeOffset.UtcNow, "updatedBy",
            false);

        var chatRoomUser = ChatRoomUser.CreateSuperUser("username");
        var chatroom = Chatroom.Load(chatroomId, new List<ChatRoomUser> { chatRoomUser }, trackerRoom);

        _ = _chatRoomServiceMock
            .Setup(x => x.Get(chatroomId))
            .ReturnsAsync(chatroom);

        _ = _chatMessageRepositoryMock
            .Setup(x => x.GetByChatRoomId(chatroomId))
            .ReturnsAsync([]);

        var result = await _chatMessageService.GetByChatRoomId(chatroomId);

        _ = result.Should().BeEmpty();
    }

    [TestMethod]
    public async Task GetByChatRoomId_WhenChatMessagesExist_ReturnsChatMessages()
    {
        var chatroomId = "chatroomId";

        var trackerRoom = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "createdBy",
            DateTimeOffset.UtcNow, "updatedBy",
            false);

        var chatRoomUser = ChatRoomUser.CreateSuperUser("username");
        var chatroom = Chatroom.Load(chatroomId, new List<ChatRoomUser> { chatRoomUser }, trackerRoom);
        _ = _chatRoomServiceMock
            .Setup(x => x.Get(chatroomId))
            .ReturnsAsync(chatroom);

        var trackerMsg = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "createdBy",
            DateTimeOffset.UtcNow, "updatedBy",
            false);

        var chatMessage1 = ChatMessage.Load(trackerMsg, "msg1", chatroomId, "user1", "Hello", DateTimeOffset.UtcNow, ChatMessageTypes.Text);
        var chatMessage2 = ChatMessage.Load(trackerMsg, "msg2", chatroomId, "user2", "Hi", DateTimeOffset.UtcNow, ChatMessageTypes.Text);
        var messages = new List<ChatMessage> { chatMessage1, chatMessage2 };

        _ = _chatMessageRepositoryMock
            .Setup(x => x.GetByChatRoomId(chatroomId))
            .ReturnsAsync(messages);

        var result = await _chatMessageService.GetByChatRoomId(chatroomId);

        _ = result.Should().HaveCount(2)
            .And.Contain([chatMessage1, chatMessage2]);
    }
}

[TestClass]
public class ChatMessageService_Create_Tests : ChatMessageServiceTestBase
{
    [TestMethod]
    public async Task Create_WhenChatroomNotFound_ThrowsBadRequestException()
    {
        var command = CreateChatMessageCommand.Create(
            "nonexistentChatroom",
            "userId",
            "Hello World",
            DateTimeOffset.UtcNow,
            ChatMessageTypes.Text);

        _ = _chatRoomServiceMock
            .Setup(x => x.Get(command.ChatroomId))
            .ReturnsAsync((Chatroom?)null);

        Func<Task> act = async () => await _chatMessageService.Create(command);

        _ = await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Chatroom not found");
    }

    [TestMethod]
    public async Task Create_WhenCommandIsValid_CreatesAndReturnsChatMessage()
    {
        var chatroomId = "chatroomId";
        var command = CreateChatMessageCommand.Create(
            chatroomId,
            "userId",
            "Hello Chat",
            DateTimeOffset.UtcNow,
            ChatMessageTypes.Text);

        var trackerRoom = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "createdBy",
            DateTimeOffset.UtcNow, "updatedBy",
            false);

        var chatRoomUser = ChatRoomUser.CreateSuperUser("userId");
        var chatroom = Chatroom.Load(chatroomId, new List<ChatRoomUser> { chatRoomUser }, trackerRoom);
        _ = _chatRoomServiceMock
            .Setup(x => x.Get(chatroomId))
            .ReturnsAsync(chatroom);

        var trackerMsg = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "createdBy",
            DateTimeOffset.UtcNow, "updatedBy",
            false);

        var createdChatMessage = ChatMessage.Load(
            trackerMsg,
            "msgId",
            chatroomId,
            command.UserId,
            command.Content,
            command.CreatedAt,
            command.Type);

        _ = _chatMessageRepositoryMock
            .Setup(x => x.Create(It.IsAny<NewChatMessage>()))
            .ReturnsAsync(createdChatMessage);

        var result = await _chatMessageService.Create(command);

        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be("msgId");
        _ = result.ChatroomId.Should().Be(chatroomId);
        _ = result.UserId.Should().Be(command.UserId);
        _ = result.Content.Should().Be(command.Content);
        _ = result.Type.Should().Be(command.Type);

        _chatMessageRepositoryMock.Verify(x => x.Create(It.IsAny<NewChatMessage>()), Times.Once);
    }
}

[TestClass]
public class ChatMessageService_Update_Tests : ChatMessageServiceTestBase
{
    [TestMethod]
    public async Task Update_WhenChatMessageDoesNotExist_ThrowsResourceNotFoundException()
    {
        var command = UpdateChatMessageCommand.Create("nonexistentMsgId", "Updated Content");
        _ = _chatMessageRepositoryMock
            .Setup(x => x.Get(command.ChatMessageId))
            .ReturnsAsync((ChatMessage?)null);

        Func<Task> act = async () => await _chatMessageService.Update(command);

        _ = await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("ChatMessage");
    }

    [TestMethod]
    public async Task Update_WhenChatMessageExists_UpdatesChatMessageSuccessfully()
    {
        var chatMessageId = "existingMsgId";
        var originalContent = "Original Content";
        var updatedContent = "Updated Content";
        var chatroomId = "chatroomId";
        var userId = "userId";
        var createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        var tracker = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "createdBy",
            DateTimeOffset.UtcNow, "updatedBy",
            false);
        var existingChatMessage = ChatMessage.Load(
            tracker,
            chatMessageId,
            chatroomId,
            userId,
            originalContent,
            createdAt,
            type);

        _ = _chatMessageRepositoryMock
            .Setup(x => x.Get(chatMessageId))
            .ReturnsAsync(existingChatMessage);

        var command = UpdateChatMessageCommand.Create(chatMessageId, updatedContent);

        await _chatMessageService.Update(command);

        _ = existingChatMessage.Content.Should().Be(updatedContent);
        _chatMessageRepositoryMock.Verify(x => x.Update(existingChatMessage), Times.Once);
    }
}

[TestClass]
public class ChatMessageService_Delete_Tests : ChatMessageServiceTestBase
{
    [TestMethod]
    public async Task Delete_WhenChatMessageDoesNotExist_ThrowsResourceNotFoundException()
    {
        var command = DeleteChatMessageCommand.Create("nonexistentMsgId");
        _ = _chatMessageRepositoryMock
            .Setup(x => x.Get(command.ChatMessageId))
            .ReturnsAsync((ChatMessage?)null);

        Func<Task> act = async () => await _chatMessageService.Delete(command);

        _ = await act.Should().ThrowAsync<ResourceNotFoundException>()
            .WithMessage("ChatMessage");
    }

    [TestMethod]
    public async Task Delete_WhenChatMessageExists_DeletesChatMessageSuccessfully()
    {
        var chatMessageId = "existingMsgId";
        var chatroomId = "chatroomId";
        var userId = "userId";
        var content = "Some Content";
        var createdAt = DateTimeOffset.UtcNow;
        var type = ChatMessageTypes.Text;

        var tracker = Tracker.LoadTracking(
            DateTimeOffset.UtcNow, "createdBy",
            DateTimeOffset.UtcNow, "updatedBy",
            false);
        var existingChatMessage = ChatMessage.Load(
            tracker,
            chatMessageId,
            chatroomId,
            userId,
            content,
            createdAt,
            type);

        _ = _chatMessageRepositoryMock
            .Setup(x => x.Get(chatMessageId))
            .ReturnsAsync(existingChatMessage);

        var command = DeleteChatMessageCommand.Create(chatMessageId);

        await _chatMessageService.Delete(command);

        _chatMessageRepositoryMock.Verify(x => x.Delete(chatMessageId), Times.Once);
    }
}