namespace ChatService.Core.Messages
{
    public class Message : MessageBase
    {
        private Message(string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
            : base(chatRoomId, userId, content, createdAt, type)
        {
        }
    }
}
