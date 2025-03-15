using AguaSensorsJSON2DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaSensorsJSON2DB.Extentions
{
    public static class HttpServiceExtentions
    {
        public static IServiceCollection AddHttpService(this IServiceCollection services,
            IConfiguration config)
        {
            var BaseAddress = config["BaseAddress"] ?? throw new Exception("BaseAddress not found");
            services.AddHttpClient<AcaWebClient>(client =>
            {
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Add("User-Agent", "AcaWebClient");
            });
            return services;
        }
    }
}
