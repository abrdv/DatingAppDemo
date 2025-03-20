using AguaSensorsJSON2DB.Extentions;
using AguaSensorsJSON2DB.Data;
using Microsoft.EntityFrameworkCore;

try
{
    /*
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build())
        .CreateLogger();
   */
    var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .AddJsonFile("appsettings.json")
        .Build();

    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder =>
        {
            builder.AddConfiguration(configuration);
        })
        //.UseSerilog()
        .ConfigureServices(services =>
        {
            services.AddApplicationServices(configuration);
            //services.AddScoped<ISensorService, SensorService>();
        }).Build();
        
    
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    {
        try
        {
            var context = scope.ServiceProvider.GetRequiredService<SensorContext>();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during migration");
        }
    }
    await host.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex );
    return 1;
}
finally
{
    //Log.CloseAndFlush();
}

return 0;