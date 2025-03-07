using ChatService.Core.Chatrooms.Models.Users;
using ChatService.Core.ChatRooms;
using ChatService.Core.ChatRooms.Commands;
using ChatService.Core.ChatRooms.Models;
using ChatService.Core.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simply.Track;

namespace ChatService.Core.Tests.Chatrooms;

public abstract class ChatRoomServiceTestBase
{
    protected Mock<ILogger<ChatRoomService>> _loggerMock = null!;
    protected Mock<IChatroomRepository> _chatRoomRepositoryMock = null!;
    protected ChatRoomService _chatRoomService = null!;

    [TestInitialize]
    public void Initialize()
    {
        _loggerMock = new Mock<ILogger<ChatRoomService>>();
        _chatRoomRepositoryMock = new Mock<IChatroomRepository>();
        _chatRoomService = new ChatRoomService(_loggerMock.Object, _chatRoomRepositoryMock.Object);
    }
}

[TestClass]
public class ChatRoomService_Get_Tests : ChatRoomServiceTestBase
{
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public async Task Get_WhenChatroomIdIsEmptyOrWhitespace_ThrowsArgumentException(string chatroomId)
    {
        Func<Task> action = async () => await _chatRoomService.Get(chatroomId);

        var errMsg = "id is required";
        _ = await action.Should().ThrowAsync<ArgumentException>().WithMessage(errMsg);
    }

    [TestMethod]
    public async Task Get_WhenChatroomNotFound_ReturnsNull()
    {
        var chatroomId = "chatroomId";

        _ = _chatRoomRepositoryMock
            .Setup(x => x.Get(chatroomId))
            .ReturnsAsync((Chatroom?)null);

        var result = await _chatRoomService.Get(chatroomId);
        _ = result.Should().BeNull();
    }

    [TestMethod]
    public async Task Get_WhenChatroomFound_ReturnsChatroom()
    {
        var chatroomId = "chatroomId";

        var tracker = Tracker.Load(
            DateTimeOffset.UtcNow,
            "createdBy",
            DateTimeOffset.UtcNow,
            "updatedBy",
            false);

        var chatroomUser = ChatRoomUser.CreateSuperUser("username");
        var chatroom = Chatroom.Load(chatroomId, [chatroomUser], tracker);

        _ = _chatRoomRepositoryMock
            .Setup(x => x.Get(chatroomId))
            .ReturnsAsync(chatroom);

        var result = await _chatRoomService.Get(chatroomId);
        _ = result.Should().Be(chatroom);
    }
}

[TestClass]
public class ChatRoomService_Create_Tests : ChatRoomServiceTestBase
{
    [TestMethod]
    public async Task Create_WhenCommandIsValid_CreatesAndReturnsChatroom()
    {
        var username = "TestUser";
        var createCommand = CreateChatRoomCommand.Create(username);

        var tracker = Tracker.Load(
            DateTimeOffset.UtcNow,
            "createdBy",
            DateTimeOffset.UtcNow,
            "updatedBy",
            false);

        var chatroomId = "chatroomId";
        var chatroomUser = ChatRoomUser.CreateSuperUser(createCommand.Username);
        var chatroom = Chatroom.Load(chatroomId, new List<ChatRoomUser> { chatroomUser }, tracker);

        _ = _chatRoomRepositoryMock
            .Setup(x => x.Create(It.IsAny<NewChatroom>()))
            .ReturnsAsync(chatroom);

        var result = await _chatRoomService.Create(createCommand);
        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be(chatroomId);
        _ = result.Users.Should().HaveCount(1);
        _ = result.Users.First()!.Id.Should().Be(chatroomUser.Id);
        _ = result.Users.First()!.Username.Should().Be(username);

        _chatRoomRepositoryMock.Verify(x => x.Create(It.IsAny<NewChatroom>()), Times.Once);
    }
}

[TestClass]
public class ChatRoomService_Update_Tests : ChatRoomServiceTestBase
{
    [TestMethod]
    public async Task Update_WhenChatroomDoesNotExist_ThrowsResourceNotFoundException()
    {
        var command = UpdateChatRoomCommand.Create("nonExistentChatroomId");

        _ = _chatRoomRepositoryMock
            .Setup(x => x.Get(command.ChatRoomId))
            .ReturnsAsync((Chatroom?)null);

        Func<Task> action = async () => await _chatRoomService.Update(command);
        _ = await action.Should().ThrowAsync<ResourceNotFoundException>();
    }

    [TestMethod]
    public async Task Update_WhenChatroomExists_UpdatesChatroomSuccessfully()
    {
        var chatroomId = "existingChatroomId";

        var existingTracker = Tracker.Load(
            DateTimeOffset.UtcNow,
            "createdBy",
            DateTimeOffset.UtcNow,
            "updatedBy",
            false);

        var chatroomUser = ChatRoomUser.CreateSuperUser("username");
        var existingChatroom = Chatroom.Load(chatroomId, new List<ChatRoomUser> { chatroomUser }, existingTracker);

        _ = _chatRoomRepositoryMock
            .Setup(x => x.Get(chatroomId))
            .ReturnsAsync(existingChatroom);

        var updateCommand = UpdateChatRoomCommand.Create(chatroomId);
        await _chatRoomService.Update(updateCommand);

        _chatRoomRepositoryMock.Verify(
            x => x.Update(It.Is<Chatroom>(cr => cr.Id == chatroomId)),
            Times.Once);
    }
}

[TestClass]
public class ChatRoomService_Delete_Tests : ChatRoomServiceTestBase
{
    [TestMethod]
    public async Task Delete_WhenChatroomDoesNotExist_ThrowsException()
    {
        var deleteCommand = DeleteChatRoomCommand.Create("nonExistentChatroomId");

        _ = _chatRoomRepositoryMock
            .Setup(x => x.Get(deleteCommand.ChatRoomId))
            .ReturnsAsync((Chatroom?)null);

        Func<Task> action = async () => await _chatRoomService.Delete(deleteCommand);
        _ = await action.Should().ThrowAsync<ResourceNotFoundException>();
    }

    [TestMethod]
    public async Task Delete_WhenChatroomExists_DeletesChatroomSuccessfully()
    {
        var deleteCommand = DeleteChatRoomCommand.Create("chatroomId");

        var tracker = Tracker.Load(
            DateTimeOffset.UtcNow,
            "createdBy",
            DateTimeOffset.UtcNow,
            "updatedBy",
            false);

        var chatroomUser = ChatRoomUser.CreateSuperUser("username");
        var existingChatroom = Chatroom.Load(deleteCommand.ChatRoomId, new List<ChatRoomUser> { chatroomUser }, tracker);

        _ = _chatRoomRepositoryMock
            .Setup(x => x.Get(deleteCommand.ChatRoomId))
            .ReturnsAsync(existingChatroom);

        await _chatRoomService.Delete(deleteCommand);
        _chatRoomRepositoryMock.Verify(
            x => x.Delete(deleteCommand.ChatRoomId),
            Times.Once);
    }
}