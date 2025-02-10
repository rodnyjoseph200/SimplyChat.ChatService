using Microsoft.Extensions.Configuration;

namespace ChatService.Infrastructure.Azure.ServiceBus;

public class AzureServiceBusOptions
{
    [ConfigurationKeyName("connection_string")]
    public required string ConnectionString { get; init; }

    [ConfigurationKeyName("queue_settings")]
    public required AzureServiceBusQueueOptions QueueOptions { get; init; }
}

public class AzureServiceBusQueueOptions
{
    [ConfigurationKeyName("queue_name")]
    public required string QueueName { get; set; }

    [ConfigurationKeyName("max_concurrent_calls")]
    public required int MaxConcurrentCalls { get; set; }
}
