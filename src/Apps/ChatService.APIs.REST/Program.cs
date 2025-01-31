using ChatService.APIs.REST;
using ChatService.Core.ChatMessages;
using ChatService.Core.ChatRooms;
using ChatService.Infrastructure.InMemoryDb.Testing.ChatMessages;
using ChatService.Infrastructure.InMemoryDb.Testing.Chatrooms;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCorsPolicy()
    .AddVersioning()
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddExceptionHandler<CustomHttpExceptionHandler>()
    .AddProblemDetails();

builder.Services.AddControllers();
builder.AddServiceDefaults();

builder.Services.AddSingleton<IChatMessageRepository, InMemoryDbChatMessageRepository>();
builder.Services.AddSingleton<IChatMessageService, ChatMessageService>();
builder.Services.AddSingleton<IChatroomRepository, InMemoryDbChatroomRepository>();
builder.Services.AddSingleton<IChatRoomService, ChatRoomService>();

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