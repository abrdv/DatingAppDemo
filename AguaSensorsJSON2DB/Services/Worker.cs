using Microsoft.EntityFrameworkCore;
using AguaSensorsJSON2DB.Data;
using Newtonsoft.Json;
using AguaSensorsJSON2DB.Entities;

namespace AguaSensorsJSON2DB.Services
{
    public class Worker : IHostedService, IHostedLifecycleService, IDisposable
    {
        private readonly ILogger<Worker> _logger;
        private Timer _timer;
        private CancellationTokenSource _stoppingCts;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly AcaWebClient _acaWebClient;
        private readonly IConfiguration _config;

        public Worker(
            ILogger<Worker> logger, 
            IServiceScopeFactory scopeFactory, 
            AcaWebClient acaWebClient,
            IConfiguration config)
        {
            _acaWebClient = acaWebClient;
            _logger = logger;
            _scopeFactory = scopeFactory;
            _config = config;
            _logger.LogDebug("Create Worker");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Worker started.");

            _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            using (var scope = _scopeFactory.CreateAsyncScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<SensorContext>();
                    context.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during migration");
                }
            }

            _timer = new Timer(async state => await DoWork(), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
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

        private async Task ProcessAguaSensors()
        {
            var response = await _acaWebClient.GetCatalogAsync();
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync() ?? throw new Exception("No JSON getting from site");
            AguaSensorJSON providers = JsonConvert.DeserializeObject<AguaSensorJSON>(json);

            if (providers != null)
            {
                using (var scope = _scopeFactory.CreateAsyncScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<SensorContext>();
                    foreach (var provider in providers.providers)
                    {
                        foreach (var sensor in provider.sensors)
                        {
                            var existingEntry = await context.AguaSensorDB.FirstOrDefaultAsync(x => x.Provider == provider.provider && x.Sensor == sensor.sensor);
                            if (existingEntry == null) 
                            {
                                await context.AguaSensorDB.AddAsync(new AguaSensorDB
                                {
                                    Provider = provider.provider,
                                    Sensor = sensor.sensor,
                                    Description = sensor.componentDesc,
                                    DataType = sensor.dataType,
                                    Location = sensor.location,
                                    Type = sensor.type,
                                    ComponentType = sensor.componentType,
                                    ComponentDesc = sensor.componentDesc,
                                    IsImportValue = false,  
                                });
                                _logger.LogInformation($"provider {provider.provider} sensor {sensor.sensor} type {sensor.type} componentDesc {sensor.componentDesc} added.");
                                Console.WriteLine($"provider {provider.provider} sensor {sensor.sensor} type {sensor.type} componentDesc {sensor.componentDesc} added");
                            }
                        }
                    }
                    await context.SaveChangesAsync();
                   
                }
            }
        }
        private async Task DoWork()
        {
            _logger.LogInformation("AguaSensor Hosted Service is working.");
            try
            {
                await ProcessAguaSensors();
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