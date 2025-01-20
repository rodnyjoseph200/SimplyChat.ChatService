namespace ChatService.Core.Messages;

public interface IChatRoomMessageRepository
{
    Task<ChatRoomMessage> Get(string id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ChatRoomMessage>> GetbyChatRoomId(string id, CancellationToken cancellationToken);

    Task<ChatRoomMessage> Add(NewChatRoomMessage message, CancellationToken cancellationToken);

}
