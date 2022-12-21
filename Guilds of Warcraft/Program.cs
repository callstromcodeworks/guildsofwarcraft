/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using CCW.GoW.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace CCW.GoW;

//TODO general code cleanup, identify debugging items that need to be removed for prod
//TODO implement caching where applicable, unit testing with 1-1000+ servers
//TODO figure out 3rd party licenses needed to include

public class Program
{
    public static readonly ApplicationConfig AppConfig = ApplicationConfig.GetConfig();
    public static readonly LogSeverity LogSeverityLevel =
#if DEBUG
        LogSeverity.Debug;
#else
        LogSeverity.Error;
#endif

    internal readonly IServiceProvider svc;

    public Program()
    {
        svc = CreateProvider();
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        new Program().Init();
    }

    /// <summary>
    /// Start the UI and worker threads
    /// </summary>
    public void Init()
    {
        Task.Run(() => new DiscordService(svc));
        Application.Run(svc.GetRequiredService<MainWindow>());
    }

    /// <summary>
    /// Dependancy Injection - Create an #IServiceProvider with the necessary services
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
        var appConfig = ApplicationConfig.GetConfig();
        var window = new MainWindow();
        return new ServiceCollection()
            .AddSingleton(new DiscordSocketClient(config))
            .AddSingleton<CommandService>()
            .AddSingleton(new DataHandler(appConfig.ConnectionString))
            .AddSingleton(window)
            .AddSingleton(new UiService(window))
            .BuildServiceProvider();
    }
}