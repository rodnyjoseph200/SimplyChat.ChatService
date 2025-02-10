using ChatService.DeleteChatroomsProcessor;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<DeleteChatroomsProcessor>();

var host = builder.Build();
host.Run();
