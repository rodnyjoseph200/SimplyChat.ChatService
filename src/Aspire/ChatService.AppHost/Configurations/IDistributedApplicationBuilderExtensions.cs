namespace ChatService.AppHost.Configurations;

public static class IDistributedApplicationBuilderExtensions
{
    public static IDistributedApplicationBuilder ConfigureAzureContainerAppEnvironment(this IDistributedApplicationBuilder builder)
    {
        return builder;
    }

    public static IDistributedApplicationBuilder ConfigureAzureLogAnalyticsWorkspace(this IDistributedApplicationBuilder builder)
    {
        return builder;
    }

    public static IDistributedApplicationBuilder ConfigureAzureApplicationInsights(this IDistributedApplicationBuilder builder)
    {
        return builder;
    }

    public static IDistributedApplicationBuilder ConfigureAzureCosmosDb(this IDistributedApplicationBuilder builder)
    {
        return builder;
    }

    public static IDistributedApplicationBuilder ConfigureAzureContainerApps(this IDistributedApplicationBuilder builder)
    {
        return builder;
    }

    public static IDistributedApplicationBuilder ConfigureAzureKeyVault(this IDistributedApplicationBuilder builder)
    {
        return builder;
    }

    public static IDistributedApplicationBuilder ConfigureAzureContainerAppForRestApi(this IDistributedApplicationBuilder builder)
    {
        return builder;
    }
}
