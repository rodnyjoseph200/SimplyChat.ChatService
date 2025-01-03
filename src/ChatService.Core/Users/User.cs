namespace ChatService.Core.Users
{
    public class User
    {
        public string Id { get; init; }
        public string Name { get; private set; }

        public string Tracker { get; set; } = string.Empty;

        private User(string username)
        {
            Id = Guid.NewGuid().ToString();
            SetName(username);
        }

        public static User Load(string username) => new(username);

        public void SetName(string username)
        {
            ValidateName(username);
            Name = username.Trim().ToLower();
        }

        public static void ValidateName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException($"{nameof(username)} is required");
            if (username.Length > 20)
                throw new ArgumentException($"{nameof(username)} is too long");
        }
    }
}
