using ChatService.Core.Messages;
using ChatService.Infrastructure.Azure.CosmosDb.Messages;

namespace ChatService.Infrastructure.Azure.CosmosDb.ChatRoomMessages;

public class CosmosDbChatRoomMessageRepository : IChatRoomMessageRepository
{
    private readonly ICosmosDbContainer _container;

    public CosmosDbChatRoomMessageRepository(ICosmosDbContainer container)
    {
        _container = container;
    }

    public Task<ChatRoomMessage> Get(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ChatRoomMessage>> GetbyChatRoomId(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ChatRoomMessage> Add(NewChatRoomMessage newMessage, CancellationToken cancellationToken)
    {
        var cosmosDbMessage = CosmosDbChatRoomMessage.Convert(newMessage);
        var response = await _container.CreateItemAsync(cosmosDbMessage, partitionKey: cosmosDbMessage.PartitionKey, cancellationToken: cancellationToken);

        //todo
        // let caller handle try/catch
        // check if success
        // add logger
        // log cost and other things in response
        // use etag
        if (response is null)
        {
            throw new Exception("Failed to create message");
        }

        var message = CosmosDbChatRoomMessage.Convert(response.Resource);
        return message;
    }
}
