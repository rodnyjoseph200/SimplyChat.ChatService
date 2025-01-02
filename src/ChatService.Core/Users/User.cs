namespace ChatService.Core.Users
{
    public class User
    {
        public string Id { get; init; }
        public string Name { get; init; }

        private User(string username)
        {
            Id = Guid.NewGuid().ToString();
            Name = username;
        }

        public static User Create(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException($"{nameof(username)} is required");

            if (username.Length > 20)
                throw new ArgumentException($"{nameof(username)} is too long");

            username = username.Trim().ToLower();

            return new User(username);
        }
    }
}
