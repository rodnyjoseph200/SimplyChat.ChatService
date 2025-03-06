using ChatService.Core.Chatrooms.Models.Users;
using ChatService.Core.ChatRooms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChatService.Core.Tests.Chatrooms.Models;

[TestClass]
public class ChatRoomUserSettingsTests
{
    [TestMethod]
    public void Create_ShouldReturnSettingsWithDefaultScheme()
    {
        var settings = ChatRoomUserSettings.Create();

        _ = settings.Should().NotBeNull();
        _ = settings.Scheme.Should().Be(ChatRoomColorSchemes.Light);
    }

    [TestMethod]
    public void Create_WithSpecifiedScheme_ShouldReturnSettingsWithThatScheme()
    {
        var scheme = ChatRoomColorSchemes.Dark;

        var settings = ChatRoomUserSettings.Create(scheme);

        _ = settings.Scheme.Should().Be(scheme);
    }

    [TestMethod]
    public void Load_ShouldReturnSettingsWithSpecifiedScheme()
    {
        var scheme = ChatRoomColorSchemes.Dark;

        var settings = ChatRoomUserSettings.Load(scheme);

        _ = settings.Scheme.Should().Be(scheme);
    }

    [TestMethod]
    public void SetScheme_ShouldChangeScheme()
    {
        var settings = ChatRoomUserSettings.Create(ChatRoomColorSchemes.Light);

        settings.SetScheme(ChatRoomColorSchemes.Dark);

        _ = settings.Scheme.Should().Be(ChatRoomColorSchemes.Dark);
    }
}