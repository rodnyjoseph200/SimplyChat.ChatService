using Microsoft.Extensions.Configuration;

namespace ChatService.Infrastructure.Azure.CosmosDB;

public class AzureCosmosDbOptions
{
    public const string Name = "AzureCosmosDb";

    [ConfigurationKeyName("connection_string")]
    public required string ConnectionString { get; init; }

    [ConfigurationKeyName("database_name")]
    public required string DatabaseName { get; init; }

    [ConfigurationKeyName("container_name")]
    public required string ContainerName { get; init; }
}
