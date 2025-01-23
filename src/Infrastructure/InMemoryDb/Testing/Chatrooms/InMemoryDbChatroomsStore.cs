using ChatService.Core.ChatRooms.Models;

namespace ChatService.Infrastructure.InMemoryDb.Testing.Chatrooms;

public static class InMemoryDbChatroomsStore
{
    private static Chatroom[] Store = [];
    public static IReadOnlyCollection<Chatroom> InMemoryDbChatrooms => Store.AsReadOnly();

    public static void Reset()
    {
        Store = [];
    }

    public static void Add(Chatroom chatroom) =>
        Store = Store.Append(chatroom).ToArray();

    public static void Remove(string id) =>
        Store = Store.Where(x => x.Id != id).ToArray();
}