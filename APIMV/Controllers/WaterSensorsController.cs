using APIMV.Data;
using APIMV.DTOs;
using APIMV.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace APIMV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaterSensorsCatalogController(DataContext dataContext, IConfiguration config) : ControllerBase
    {
        private readonly HttpClient _httpClient = new HttpClient();

        [HttpGet]
        public async Task<IActionResult> GetWaterSensors()
        {
            var urlWaterSensors = config["URLWaterSensors"] ?? throw new Exception("Cannot access URLWaterSensors from appsettings");
            try
            {
                // Получение JSON данных
                var response = await _httpClient.GetAsync(urlWaterSensors);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                // Десериализация JSON в список объектов
                var entities = JsonConvert.DeserializeObject<SensorJSON>(json);

                if (entities == null) throw new Exception("Not found data");
                if (entities.providers == null) throw new Exception("Not found data");

                // Обновление данных в базе данных
                foreach (var provider in entities.providers)
                {
                    if (provider != null)
                    {
                        foreach (var sensorElement in provider.sensors)
                        {
                            if (sensorElement != null)
                            {
                                if (dataContext.WaterSensors.FirstAsync(_sensor => _sensor.sensor == sensorElement.sensor))
                            }

                        }
                    }

                }
                    /*
                    // Обновление данных в базе данных
                    foreach (var entity in entities)
                    {
                        //dataContext.WaterSensors.Update(entity);
                        if (entity != null)
                        {
                            Console.WriteLine(string.Join(",", entity));
                        }
                    }
                    //await dataContext.SaveChangesAsync();
                    */
                    return Ok("Данные успешно обновлены");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
            
        }

    }
}
