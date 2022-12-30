/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */
using CCW.GoW.DataService;
using MessagePipe;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CCW.GoW;

//TODO general code cleanup, identify debugging items that need to be removed for prod
//TODO implement caching where applicable, unit testing with 1-1000+ servers
//TODO figure out 3rd party licenses needed to include

public class Program
{
    static readonly IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
            }).Build();

    /// <summary>
    /// Creates a <see cref="ServiceProvider"/> with the necessary services
    /// </summary>
    /// <returns>The <see cref="ServiceProvider"/>.</returns>
    static IServiceProvider CreateProvider()
    {
        return new ServiceCollection()
            .AddMessagePipe()
            .AddMessagePipeNamedPipeInterprocess("CCW-GoW")
            .BuildServiceProvider();
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.ApplicationExit += Exit;
        host.StartAsync();
        Application.Run(new MainWindow(CreateProvider()));
    }
    public static void Exit(object? sender, EventArgs e)
    {
        host.StopAsync();
    }
}