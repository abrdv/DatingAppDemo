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
            decimal valueDecimal = 0;
            DateTime dateTime;
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
                            var json = await response.Content.ReadAsStringAsync();
                            if (json == null) 
                            { 
                                continue; 
                            }
                            AguaSensorDataJSON? dataJSON = JsonConvert.DeserializeObject<AguaSensorDataJSON>(json);
                            if (dataJSON?.observations[0]?.value == null)
                                {
                                continue;
                                } else 
                                {
                                    valueDecimal = dataJSON.observations[0].value ?? 0m;
                                }
                            if (dataJSON?.observations[0]?.timestamp == null)
                            {
                                continue;
                            }
                            else
                            {
                                if (DateTime.TryParseExact(dataJSON?.observations[0]?.timestamp, "dd/MM/yyyyTHH:mm:ss", null, System.Globalization.DateTimeStyles.None, out dateTime))
                                {
                                    Console.WriteLine($"dateTime = {dateTime}");
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            await context.AguaSensorData.AddAsync(new AguaSensorData
                            {
                                AguaSensorDBId = entry.Id,
                                Provider = entry.Provider ?? "",
                                Sensor = entry.Sensor ?? "",
                                Description = entry.Description ?? "",
                                Value = valueDecimal,
                                TimeStamp = dateTime,
                                Date = date,
                            });
                             Console.WriteLine($"Id {entry.Id} Provider {entry.Provider} Sensor {entry.Sensor} valueDecimal {valueDecimal} added");
                            }
                        }
                    }
                context.SaveChangesAsync();
            }

        }
    }
}
