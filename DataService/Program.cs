using CCW.GoW.DataService;

IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    }).Build();

host.Run();