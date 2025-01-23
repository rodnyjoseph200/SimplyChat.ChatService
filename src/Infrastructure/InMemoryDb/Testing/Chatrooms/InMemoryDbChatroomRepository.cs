using ChatService.Core.ChatRooms;
using ChatService.Core.ChatRooms.Models;
using Microsoft.Extensions.Logging;
using Simply.Track;

namespace ChatService.Infrastructure.InMemoryDb.Testing.Chatrooms;
public class InMemoryDbChatroomRepository : IChatroomRepository
{
    private readonly ILogger<InMemoryDbChatroomRepository> _logger;

    public InMemoryDbChatroomRepository(ILogger<InMemoryDbChatroomRepository> logger)
    {
        _logger = logger;
    }

    public async Task<Chatroom?> Get(string id)
    {
        _logger.LogInformation("Getting chatroom");
        var chatroom = InMemoryDbChatroomsStore.InMemoryDbChatrooms.SingleOrDefault(x => x.Id == id);
        if (chatroom is not null)
            _logger.LogInformation("Chatroom found");

        await Task.CompletedTask;
        return chatroom;
    }

    public async Task<Chatroom> Create(NewChatroom newChatRoom)
    {
        _logger.LogInformation("Creating chatroom");

        var chatroomUserSettings = ChatRoomUserSettings.Load(ChatRoomColorSchemes.Light);
        var chatroomUser = ChatRoomUser.Load(Guid.NewGuid().ToString(), chatroomUserSettings, isSuperUser: true);

        var chatroom = Chatroom.Load(Guid.NewGuid().ToString(), new List<ChatRoomUser>() { chatroomUser },
            Tracker.LoadTracking(DateTimeOffset.UtcNow, "test", DateTimeOffset.UtcNow, "test", false, null, null, null, null));

        InMemoryDbChatroomsStore.Add(chatroom);
        _logger.LogInformation("Chatroom created");

        await Task.CompletedTask;
        return chatroom;
    }

    public async Task Update(Chatroom chatRoom)
    {
        _logger.LogInformation("Updating chatroom");
        InMemoryDbChatroomsStore.Remove(chatRoom.Id);
        InMemoryDbChatroomsStore.Add(chatRoom);
        _logger.LogInformation("Chatroom updated");

        await Task.CompletedTask;
    }

    public async Task Delete(string id)
    {
        _logger.LogInformation("Deleting chatroom");
        InMemoryDbChatroomsStore.Remove(id);
        _logger.LogInformation("Chatroom deleted");

        await Task.CompletedTask;
    }
}
