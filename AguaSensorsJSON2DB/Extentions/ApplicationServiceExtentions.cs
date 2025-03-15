using AguaSensorsJSON2DB.Data;
using AguaSensorsJSON2DB.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaSensorsJSON2DB.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddHttpService(config);

            services.AddDbContext<SensorContext>(opt =>
            {
                opt.UseSqlite(config["DefaultConnection"]);
            });
            services.AddLogging();
            services.AddHttpClient();

            services.AddHostedService<Worker>();
            return services;
        }
    }
}
