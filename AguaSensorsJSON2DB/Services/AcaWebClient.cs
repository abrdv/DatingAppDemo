using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaSensorsJSON2DB.Services
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
            var CatalogAddress = _config["CatalogAddress"] ?? throw new Exception("Cannot access CatalogAddress from appsettings");
            return await _httpClient.GetAsync(CatalogAddress);
        }
    }
}
