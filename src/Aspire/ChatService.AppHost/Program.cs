var builder = DistributedApplication.CreateBuilder(args);

var projectPath = builder.Environment.ContentRootPath;

const string bicepMainPath = "../../../../deploy/azure/";
const string bicepSharedPath = $"{bicepMainPath}/deploy/azure/shared/";
const string cosmosDbConnectionStringName = "cosmosdb";

var restApi = builder.AddProject<Projects.ChatService_APIs_REST>("chatservice-rest-api");

if (builder.ExecutionContext.IsPublishMode)
{
    var keyVault = builder.AddBicepTemplate("keyvault", $"{bicepSharedPath}keyvault.bicep");
    var keyVaultSecret = builder.AddBicepTemplate("keyvault-secret", $"{bicepSharedPath}keyvault-secret.bicep");
    var logAnalyticsWorkspace = builder.AddBicepTemplate("loganalytics-workspace", $"{bicepSharedPath}loganalytics-workspace.bicep");
    var appInsights = builder.AddBicepTemplate("applicationinsights", $"{bicepSharedPath}applicationinsights.bicep");
    var containerAppEnvironment = builder.AddBicepTemplate("containerapp-environment", $"{bicepSharedPath}containerapp-environment.bicep");
    var restApicontainerApp = builder.AddBicepTemplate("containerapps", $"{bicepSharedPath}containerapps.bicep");

    var cosmos = builder.AddBicepTemplate(cosmosDbConnectionStringName, $"{bicepSharedPath}cosmosdb.bicep")
        .WithParameter("databaseAccountName", "todo")
        .WithParameter("environmentName", "eastus")
        .WithParameter("appName", "todo");

    var cosmosConnectionString = cosmos.GetSecretOutput("cosmosConnectionString");
    _ = restApi.WithEnvironment($"ConnectionStrings__{cosmosDbConnectionStringName}", cosmosConnectionString);
}
else
{
    var cosmosdb = builder.AddConnectionString(cosmosDbConnectionStringName);
    _ = restApi.WithReference(cosmosdb);
}

//var cosmos = builder.AddBicepTemplate("cosmos", "../../../../infra/cosmosdb.bicep")
//    .WithParameter("databaseAccountName", "fallout-db")
//    .WithParameter(AzureBicepResource.KnownParameters.KeyVaultName)
//    .WithParameter("databases", ["vault-33", "vault-111"]);

builder.Build().Run();