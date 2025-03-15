using Microsoft.EntityFrameworkCore;
using Serilog;
using AguaSensorsJSON2DB.Data;
using AguaSensorsJSON2DB;
using AguaSensorsJSON2DB.Services;
using AguaSensorsJSON2DB.Extentions;

try
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build())
        .CreateLogger();
    
    var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .AddJsonFile("appsettings.json")
        .Build();

    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder => {
            builder.AddConfiguration(configuration);
        })
        .UseSerilog()
        .ConfigureServices(services =>
        {
            services.AddApplicationServices(configuration);
            //services.AddScoped<ISensorService, SensorService>();
        })
        .Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

return 0;