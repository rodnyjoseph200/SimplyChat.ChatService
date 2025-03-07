using Microsoft.Extensions.DependencyInjection;

namespace ChatService.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        _ = services
            .AddServices();

        return services;
    }
}