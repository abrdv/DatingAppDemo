using APIMV.Data;
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
            try
            {

                    return Ok("Данные успешно обновлены");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }
            
        }

    }
}
