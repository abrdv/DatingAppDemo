namespace AguaSensorsJSON2DB.Helpers
{
    public class AcaWebClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AcaWebClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<HttpResponseMessage> GetCatalogAsync()
        {
            var CatalogAddress = _config["Url:CatalogAddress"] ?? throw new Exception("Cannot access CatalogAddress from appsettings");
            return await _httpClient.GetAsync(CatalogAddress);
        }
        public async Task<HttpResponseMessage> GetDataAsync(string provider, string sensor)
        {
            var DataAddress = _config["Url:DataAddress"] ?? throw new Exception("Cannot access DataAddress from appsettings");
            DataAddress = DataAddress + provider + "/" + sensor;
            return await _httpClient.GetAsync(DataAddress);
        }
    }
}
