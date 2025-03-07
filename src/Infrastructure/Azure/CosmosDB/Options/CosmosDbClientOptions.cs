using Microsoft.Extensions.Configuration;

namespace ChatService.Infrastructure.Azure.CosmosDB.Options;

public record CosmosDbClientOptions
{
    public const string Name = "cosmos_db_settings";

    [ConfigurationKeyName("connection_string")]
    public string ConnectionString { get; init; } = string.Empty;

    [ConfigurationKeyName("database_name")]
    public string DatabaseId { get; init; } = string.Empty;

    [ConfigurationKeyName("container_name")]
    public string ContainerId { get; init; } = string.Empty;

    public static CosmosDbClientOptions Validate(CosmosDbClientOptions? options)
    {
        if (options is null)
            throw new Exception($"Could not bind to {nameof(CosmosDbClientOptions)} given the name {Name}");

        if (string.IsNullOrWhiteSpace(options.ConnectionString))
            throw new InvalidOperationException("Cosmos DB connection string is missing");
        if (string.IsNullOrWhiteSpace(options.DatabaseId))
            throw new InvalidOperationException("Cosmos DB database ID is missing");
        if (string.IsNullOrWhiteSpace(options.ContainerId))
            throw new InvalidOperationException("Cosmos DB container ID is missing");

        return options;
    }
}