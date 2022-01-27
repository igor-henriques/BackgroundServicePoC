namespace BackgroundServicePoC.Services;

public sealed class MainWorker : BackgroundService
{
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly ILogger<MainWorker> _logger;

    public MainWorker(IBackgroundTaskQueue taskQueue, ILogger<MainWorker> logger)
        => (_taskQueue, _logger) = (taskQueue, logger);

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(MainWorker)} inicializado");

        return ProcessTaskQueueAsync(stoppingToken);
    }

    private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await _taskQueue.DequeueAsync(stoppingToken);

            await workItem(stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(MainWorker)} finalizado.");

        await base.StopAsync(stoppingToken);
    }
}