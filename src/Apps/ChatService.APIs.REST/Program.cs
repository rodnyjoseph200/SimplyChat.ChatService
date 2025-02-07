using ChatService.APIs.REST;
using ChatService.Infrastructure.Azure.CosmosDB;
using ChatService.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Automatic service discovery (annotated with a Service attribute)
// Adds services (singleton by default) with no need for manual registration
builder.Services.AddServices();

builder.Services
    .AddCorsPolicy()
    .AddVersioning()
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddExceptionHandler<CustomHttpExceptionHandler>()
    .AddProblemDetails();

var cosmosConnectionString = builder.Configuration["cosmos-connection-string"];
if (string.IsNullOrWhiteSpace(cosmosConnectionString))
    throw new InvalidOperationException("Cosmos DB connection string is missing");

var databaseId = builder.Configuration["cosmos-database-name"];
if (string.IsNullOrWhiteSpace(databaseId))
    throw new InvalidOperationException("Cosmos DB database ID is missing");

var containerId = builder.Configuration["cosmos-container-name"];
if (string.IsNullOrWhiteSpace(containerId))
    throw new InvalidOperationException("Cosmos DB container ID is missing");

builder.Services.AddAzureCosmosDb(cosmosConnectionString, databaseId, containerId);

builder.Services.AddControllers();
builder.AddServiceDefaults();

var app = builder.Build();

var serviceProvider = app.Services;
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

try
{
    _ = app.MapDefaultEndpoints();
    _ = app.UseExceptionHandler();

    // In a real-world scenario, Swagger should not be enabled in production
    //if (!app.Environment.IsProduction())
    //{
    _ = app
        .UseSwagger()
        .UseSwaggerUI();
    //}

    _ = app
    .UseHttpsRedirection()
    .UseCors(ServiceCollectionExtensions.CORS_POLICY_NAME)
    .UseAuthorization();

    _ = app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.LogError(e, "Could not run service");
}