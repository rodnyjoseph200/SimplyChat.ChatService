using ChatService.Core.ChatMessages.Models;

namespace ChatService.Infrastructure.InMemoryDb.Testing.ChatMessages;

public class InMemoryDbChatMessagesStore
{
    private static ChatMessage[] Store = [];
    public static IReadOnlyCollection<ChatMessage> InMemoryDbChatMessages => Store.AsReadOnly();

    public static void Reset()
    {
        Store = [];
    }

    public static void Add(ChatMessage chatMessage) =>
        Store = Store.Append(chatMessage).ToArray();

    public static void Remove(string id) =>
        Store = Store.Where(x => x.Id != id).ToArray();

}
