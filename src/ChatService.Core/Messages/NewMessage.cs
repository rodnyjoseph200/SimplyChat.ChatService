
namespace ChatService.Core.Messages
{
    public class NewMessage : MessageBase
    {
        public NewMessage(string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
            : base(chatRoomId, userId, content, createdAt, type)
        {
        }
    }
}
