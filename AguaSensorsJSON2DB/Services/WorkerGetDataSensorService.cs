using Microsoft.EntityFrameworkCore;
using AguaSensorsJSON2DB.Data;
using Newtonsoft.Json;
using AguaSensorsJSON2DB.Entities;
using AguaSensorsJSON2DB.Helpers;
using AguaSensorsJSON2DB.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AguaSensorsJSON2DB.Services
{
    internal class WorkerGetDataSensorService : WorkerServiceBase
    {
        public WorkerGetDataSensorService(
            ILogger<WorkerServiceBase> logger,
            IServiceScopeFactory scopeFactory,
            AcaWebClient acaWebClient,
            IConfiguration config)
        {
            this._logger = logger;
            this._scopeFactory = scopeFactory;
            this._acaWebClient = acaWebClient;
            this._config = config;
            string WorkerSpanTimeStr = _config["Worker:GetDataSensorSpanTime"] ?? "10000";
            int.TryParse(WorkerSpanTimeStr, out this.WorkerSpanTime);
            string WorkerStartTimeStr = _config["Worker:GetDataSensorStartTime"] ?? "10000";
            int.TryParse(WorkerStartTimeStr, out this.WorkerStartTime);
            _logger.LogInformation("Create Worker");
        }

        protected override async Task Process()
        {
            //
            decimal valueInt = 0;
            DateTime date = DateTime.UtcNow.Date;
            using (var scope = _scopeFactory.CreateAsyncScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SensorContext>();
                if (await context.AguaSensorData.CountAsync(x => x.Date == date) != 0) return;

                var existingEntry = await context.AguaSensorDB.Where(x => x.IsImportValue == true).ToArrayAsync();
                if (existingEntry != null)
                {
                    
                    foreach (AguaSensorDB entry in existingEntry)
                    {
                        var existingDBData = await context.AguaSensorData.FirstOrDefaultAsync(x => x.Date == date && x.AguaSensorDBId == entry.Id);
                        if (existingDBData == null)
                        {
                            var response = await base._acaWebClient.GetDataAsync(entry.Provider, entry.Sensor);
                            response.EnsureSuccessStatusCode();
                            var json = await response.Content.ReadAsStringAsync() ?? throw new Exception("No JSON getting from site");
                            AguaSensorDataJSON? dataJSON = JsonConvert.DeserializeObject<AguaSensorDataJSON>(json);
                            if (dataJSON != null)
                            {
                                //json = {"observations":[{"value":"42.009","timestamp":"20/03/2025T07:50:00","location":""}]}
                                if (dataJSON.observations.Count() > 0 &&) {
                                    var value = dataJSON.observations[0];
                                    //Console.WriteLine($"dataJSON = {dataJSON.observations[0].value}, value = {value}");

                                    if (value != null)
                                    {
                                        await context.AguaSensorData.AddAsync(new AguaSensorData
                                        {
                                            AguaSensorDBId = entry.Id,
                                            Provider = entry.Provider,
                                            Sensor = entry.Sensor,
                                            Description = entry.Description,
                                            Value = dataJSON.observations[0].value,
                                            Date = date,
                                        });
                                        Console.WriteLine($"Id {entry.Id} Provider {entry.Provider} Sensor {entry.Sensor} valueInt {valueInt} added");
                                    }
                                }
                            }
                        }
                    }
                    
                }
                context.SaveChangesAsync();
            }

        }
    }
}
