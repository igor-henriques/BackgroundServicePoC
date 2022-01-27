using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        services.AddSingleton<Application>();
        services.AddHostedService<MainWorker>();
        services.AddSingleton<IBackgroundTaskQueue>(_ =>
        {
            return new BackgroundTaskQueue(100);
        });
    })
    .Build();

Application app = host.Services.GetRequiredService<Application>()!;

app.Start();

await host.RunAsync();