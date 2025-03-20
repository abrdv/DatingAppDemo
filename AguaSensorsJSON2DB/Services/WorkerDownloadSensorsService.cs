using Microsoft.EntityFrameworkCore;
using AguaSensorsJSON2DB.Data;
using Newtonsoft.Json;
using AguaSensorsJSON2DB.Entities;
using AguaSensorsJSON2DB.Helpers;

namespace AguaSensorsJSON2DB.Services
{
    public class WorkerDownloadSensorsService : WorkerServiceBase
    {
        public WorkerDownloadSensorsService(
            ILogger<WorkerServiceBase> logger,
            IServiceScopeFactory scopeFactory,
            AcaWebClient acaWebClient,
            IConfiguration config)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;
            this._acaWebClient = acaWebClient;
            this._config = config;
            string WorkerSpanTimeStr = _config["Worker:DownloadSensorsSpanTime"] ?? "10000";
            int.TryParse(WorkerSpanTimeStr, out WorkerSpanTime );
            string WorkerStartTimeStr = _config["Worker:DownloadSensorsStartTime"] ?? "10000";
            int.TryParse(WorkerStartTimeStr, out WorkerStartTime);
            _logger.LogInformation("Create Worker");
        }

        protected override async Task Process()
        {
            var response = await base._acaWebClient.GetCatalogAsync();
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync() ?? throw new Exception("No JSON getting from site");
            AguaSensorJSON? providers = JsonConvert.DeserializeObject<AguaSensorJSON>(json);

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
    }
}
