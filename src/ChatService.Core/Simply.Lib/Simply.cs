namespace ChatService.Core.Simply.Lib;

/// <summary>
/// Move to NuGet package
/// </summary>
public static partial class Simply
{
    public static class StringIsNullOrWhitespace
    {
        /// <summary>
        /// Throw ArgumentException if string is null or whitespace
        /// </summary>
        /// <param name="args">arg name and value</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ThrowArgumentException(params (string, string)[] args)
        {
            var errMsg = args.Where(x => string.IsNullOrWhiteSpace(x.Item2))
                .Select(x => $"{x.Item1} is required.")
                .ToList();
            throw new ArgumentException(string.Join(" ", errMsg));
        }
    }
}
