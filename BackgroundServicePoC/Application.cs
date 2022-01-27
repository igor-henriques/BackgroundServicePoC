public class Application
{
    private readonly List<Bolsa> _bolsasPendentes = Bolsa.GenerateList();
    private readonly List<Bolsa> _bolsasFalhaProcessamento = new();

    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly ILogger<Application> _logger;
    private readonly CancellationToken _cancellationToken;

    public Application(IBackgroundTaskQueue taskQueue, ILogger<Application> logger, IHostApplicationLifetime applicationLifetime)
        =>  (_taskQueue, _logger, _cancellationToken) = (taskQueue, logger, applicationLifetime.ApplicationStopping);
    
    public void Start()
    {
        _logger.LogInformation($"{nameof(Application)} inicializado.");

        Task.Run(async () => await PostCollectionIntoQueue());
    }

    private async ValueTask PostCollectionIntoQueue()
    {
        foreach (var bolsa in _bolsasPendentes)
        {
            if (_cancellationToken.IsCancellationRequested) break;

            var taskBolsa = BuildTask(bolsa);

            _logger.LogInformation($"Enfileirando task de processamento de bolsa ID {bolsa.Id}. " +
                $"Tempo previsto de processamento: {bolsa.EstimatedTaskDuration}ms");

            await _taskQueue.QueueBackgroundWorkItemAsync(taskBolsa);
        }
    }

    private Func<CancellationToken, ValueTask> BuildTask(Bolsa bolsa)
    {
        var task = new Func<CancellationToken, ValueTask>(async (token) =>
        {
            try
            {
                Stopwatch sw = new();

                sw.Start();

                await Task.Delay((int)bolsa.EstimatedTaskDuration, token);

                if (bolsa.Id is 2) throw new Exception("Teste de exception");

                _logger.LogInformation($"Processamento de bolsa ID {bolsa.Id} concluído após " +
                    $"{sw.ElapsedMilliseconds}ms versus {bolsa.EstimatedTaskDuration}ms estimados");
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu um erro de processamento da bolsa ID {bolsa.Id}:\n{e}");

                //Possíveis log em banco, caso necessário
            }            
        });

        return task;
    }
}