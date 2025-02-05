using Aspire.Hosting;

namespace ChatService.Infrastructure.Azure.IaC.CosmosDB;

public static class IDistributedApplicationBuilderExtensions
{
    public static IDistributedApplicationBuilder ConfigureAzureCosmosDb(this IDistributedApplicationBuilder builder, string bicepFileFullPath, out string cosmosConnectionString)
    {
        cosmosConnectionString = string.Empty;
        //var cosmos = builder.AddBicepTemplate(CosmosDbConfig.COSMOS_NAME, bicepFileFullPath)
        //.WithParameter("databaseAccountName", "todo")
        //.WithParameter("environmentName", builder.Environment.EnvironmentName)
        //.WithParameter("location", "eastus")
        //.WithParameter("appName", "todo");

        //cosmosConnectionString = cosmos.GetSecretOutput("cosmosConnectionString");

        return builder;
    }
}
