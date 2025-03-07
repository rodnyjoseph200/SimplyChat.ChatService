using ChatService.APIs.REST;
using ChatService.Core;
using ChatService.Infrastructure;
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
    .AddProblemDetails()
    .AddCoreDependencies()
    .AddInfrastructureDependencies(builder.Configuration)
    .AddControllers();

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
    .UseCors(DefaultServiceCollectionExtensions.CORS_POLICY_NAME)
    .UseAuthorization();

    _ = app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.LogError(e, "Could not run service");
}