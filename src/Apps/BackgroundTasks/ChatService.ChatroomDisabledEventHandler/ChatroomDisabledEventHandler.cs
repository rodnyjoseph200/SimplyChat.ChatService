namespace ChatService.ChatroomDisabledEventHandler;

public class ChatroomDisabledEventHandler : BackgroundService
{
    private readonly ILogger<ChatroomDisabledEventHandler> _logger;

    public ChatroomDisabledEventHandler(ILogger<ChatroomDisabledEventHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
