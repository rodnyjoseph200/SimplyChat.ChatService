﻿using ChatService.Core.ChatRooms;
using Guarded.Guards;

namespace ChatService.Core.Chatrooms.Models.Users;

public record ChatRoomUser
{
    public ID Id { get; }
    public string Username { get; }
    public ChatRoomUserSettings Settings { get; }
    public bool IsSuperUser { get; }

    private ChatRoomUser(ID id, string username, ChatRoomUserSettings settings, bool isSuperUser)
    {
        _ = Guard.AgainstNullsAndWhitespaces(username, settings);

        Id = id;
        Username = username;
        Settings = settings;
        IsSuperUser = isSuperUser;
    }

    public static ChatRoomUser Create(string username, bool isSuperUser = false) => new(ID.Generate, username, ChatRoomUserSettings.Create(ChatRoomColorSchemes.Light), isSuperUser);

    public static ChatRoomUser CreateSuperUser(string username) => Create(username, true);

    public static ChatRoomUser Load(ID id, string username, ChatRoomUserSettings settings, bool isSuperUser) => new(id, username, settings, isSuperUser);
}