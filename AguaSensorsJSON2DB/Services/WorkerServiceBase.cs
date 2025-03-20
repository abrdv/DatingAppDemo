using AguaSensorsJSON2DB.Helpers;

namespace AguaSensorsJSON2DB.Services
{
    public class WorkerServiceBase : IHostedService, IHostedLifecycleService, IDisposable
    {
        protected ILogger<WorkerServiceBase> _logger;
        protected Timer _timer;
        protected CancellationTokenSource _stoppingCts;
        protected IServiceScopeFactory _scopeFactory;
        protected AcaWebClient _acaWebClient;
        protected IConfiguration _config;
        protected int WorkerSpanTime;
        protected int WorkerStartTime;
        
        /*
        public WorkerServiceBase(
            ILogger logger,
            IServiceScopeFactory scopeFactory,
            AcaWebClient acaWebClient,
            IConfiguration config)
        {
            this._acaWebClient = acaWebClient;
            this._logger = logger;
            this._scopeFactory = scopeFactory;
            this._config = config;
            _logger.LogDebug("Create Worker");
        }
        */
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Worker started.");

            _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            Console.WriteLine($"WorkerStartTime={WorkerStartTime}, WorkerSpanTime={WorkerSpanTime}");
            _timer = new Timer(async state => await DoWork(), null, TimeSpan.FromSeconds(WorkerStartTime), TimeSpan.FromSeconds(WorkerSpanTime));
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            _stoppingCts?.Cancel();
            return Task.CompletedTask;
        }

        public Task StartingAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker starting lifecycle.");
            return Task.CompletedTask;
        }

        public Task StartedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker started lifecycle.");
            return Task.CompletedTask;
        }

        public Task StoppingAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopping lifecycle.");
            return Task.CompletedTask;
        }

        public Task StoppedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopped lifecycle.");
            return Task.CompletedTask;
        }

        protected virtual async Task Process()
        {
        }
        protected async Task DoWork()
        {
            _logger.LogInformation("Service is working.");
            try
            {
                await Process();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing AguaSensors.");
            }
        }
        public void Dispose()
        {
            _timer?.Dispose();
            _stoppingCts?.Dispose();
            _logger.LogInformation("Worker disposed.");
        }
    }
}