using AguaSensorsJSON2DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace AguaSensorsJSON2DB.Data
{
    public class SensorContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AguaSensorData> AguaSensorData { get; set; }
        public DbSet<AguaSensorDB> AguaSensorDB { get; set; }
        
    }
    
}
