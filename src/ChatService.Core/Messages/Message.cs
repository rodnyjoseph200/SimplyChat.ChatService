namespace ChatService.Core.Messages
{
    public class Message : MessageBase
    {
        public string Tracker { get; set; } = string.Empty;

        private Message(string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
            : base(chatRoomId, userId, content, createdAt, type)
        {
        }

        public static Message Load(string chatRoomId, string userId, string content, DateTimeOffset createdAt, int type)
        {
            return new Message(chatRoomId, userId, content, createdAt, type);
        }
    }
}
