using Discord.Commands;
using Discord.WebSocket;
using Discord;
using MessagePipe;

namespace CCW.GoW.DataService;

public class Worker : BackgroundService
{
    public static readonly LogSeverity LogSeverityLevel =
#if DEBUG
        LogSeverity.Debug;
#else
    LogSeverity.Error;
#endif

    public static readonly ApplicationConfig AppConfig = ApplicationConfig.GetConfig();
    private readonly IServiceProvider services;
    public Worker()
    {
        services = CreateProvider();
    }

    /// <summary>
    /// Dependancy Injection - Create an IServiceProvider with the necessary services
    /// </summary>
    /// <returns></returns>
    static IServiceProvider CreateProvider()
    {
        var config = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged,
            LogLevel = LogSeverityLevel,
            DefaultRetryMode = RetryMode.RetryTimeouts | RetryMode.Retry502 | RetryMode.RetryRatelimit
        };
        return new ServiceCollection()
            .AddMessagePipe()
            .AddMessagePipeNamedPipeInterprocess("CCW-GoW")
            .AddSingleton(new DiscordSocketClient(config))
            .AddSingleton<CommandService>()
            .AddSingleton(new DataHandler(AppConfig.ConnectionString))
            .BuildServiceProvider();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var discordService = new DiscordService(services);
            await discordService.RunAsync();
        }
    }
}