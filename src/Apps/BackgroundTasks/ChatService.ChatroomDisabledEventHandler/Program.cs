using ChatService.ChatroomDisabledEventHandler;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ChatroomDisabledEventHandler>();

var host = builder.Build();
host.Run();
