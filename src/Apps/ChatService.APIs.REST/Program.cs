using ChatService.APIs.REST;

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

builder.Services.AddControllers();
builder.AddServiceDefaults();

var app = builder.Build();

var serviceProvider = app.Services;
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

try
{
    _ = app.MapDefaultEndpoints();
    _ = app.UseExceptionHandler();

    if (!app.Environment.IsProduction())
    {
        _ = app
        .UseSwagger()
        .UseSwaggerUI();
    }

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