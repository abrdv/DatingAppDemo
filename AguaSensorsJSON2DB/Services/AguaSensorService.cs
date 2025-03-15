
using AguaSensorsJSON2DB.Interfaces;
using AguaSensorsJSON2DB.Data;
using System.Net.Http.Json;


namespace AguaSensorsJSON2DB.Services
{
    public class AguaSensorService : ISensorService
    {
        private readonly SensorContext _context;
        private readonly ILogger<AguaSensorService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AguaSensorService(
               SensorContext context, 
               ILogger<AguaSensorService> logger, 
               IHttpClientFactory httpClientFactory, 
               IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task GetRemoteSensors()
        {
            using (HttpClient client = new HttpClient())
            {
                
                var response = await client.GetAsync(_configuration["BaseAddress"]+ _configuration["CatalogAddress"]);
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadFromJsonAsync<AguaSensorJSON>();
                    //if (json != null) return json;
                }
            }
            //return null;
        }

        public Task MatchingRemoteSensors()
        {
            throw new NotImplementedException();
        }
    }
}
