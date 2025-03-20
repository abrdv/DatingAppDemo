using AguaSensorsJSON2DB.Data;
using AguaSensorsJSON2DB.Services;
using Microsoft.EntityFrameworkCore;

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
                opt.UseSqlite(config["ConnectionStrings:DefaultConnection"]);
            });
            services.AddLogging();
            services.AddHttpClient();
            services.AddHostedService<WorkerDownloadSensorsService>();
            services.AddHostedService<WorkerGetDataSensorService>();

            return services;
        }
    }
}
