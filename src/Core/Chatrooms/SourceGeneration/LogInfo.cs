using Microsoft.Extensions.Logging;

namespace ChatService.Core.Chatrooms.SourceGeneration;

/// <summary>
/// Example using Source Generation logging
/// </summary>
public static partial class LogInfo
{
    public const LogLevel Level = LogLevel.Information;

    public enum ChatroomServiceEventId
    {
        GettingChatroomById,
        ChatroomFound,
        ChatroomNotfound,
        CreatingChatroom,
        ChatroomCreated,
        UpdatingChatroom,
        ChatroomUpdated,
        DeletingChatroom,
        ChatroomDeleted
    }

    [LoggerMessage(
        EventId = (int)ChatroomServiceEventId.GettingChatroomById,
        Level = Level,
        Message = "Getting chatroom by id")]
    public static partial void GettingChatroomById(ILogger logger);

    [LoggerMessage(
        EventId = (int)ChatroomServiceEventId.ChatroomFound,
        Level = Level,
        Message = "Chatroom found")]
    public static partial void ChatroomFound(ILogger logger);

    [LoggerMessage(
        EventId = (int)ChatroomServiceEventId.ChatroomNotfound,
        Level = Level,
        Message = "Chatroom not found")]
    public static partial void ChatroomNotfound(ILogger logger);

    [LoggerMessage(
        EventId = (int)ChatroomServiceEventId.CreatingChatroom,
        Level = Level,
        Message = "Creating chatroom")]
    public static partial void CreatingChatroom(ILogger logger);

    [LoggerMessage(
        EventId = (int)ChatroomServiceEventId.ChatroomCreated,
        Level = Level,
        Message = "Chatroom created")]
    public static partial void ChatroomCreated(ILogger logger);

    [LoggerMessage(
        EventId = (int)ChatroomServiceEventId.UpdatingChatroom,
        Level = Level,
        Message = "Updating chatroom")]
    public static partial void UpdatingChatroom(ILogger logger);

    [LoggerMessage(
        EventId = (int)ChatroomServiceEventId.ChatroomUpdated,
        Level = Level,
        Message = "Chatroom updated")]
    public static partial void ChatroomUpdated(ILogger logger);

    [LoggerMessage(
        EventId = (int)ChatroomServiceEventId.DeletingChatroom,
        Level = Level,
        Message = "Deleting chatroom")]
    public static partial void DeletingChatroom(ILogger logger);

    [LoggerMessage(
        EventId = (int)ChatroomServiceEventId.ChatroomDeleted,
        Level = Level,
        Message = "Chatroom deleted")]
    public static partial void ChatroomDeleted(ILogger logger);

}