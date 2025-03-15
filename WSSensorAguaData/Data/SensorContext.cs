using Microsoft.EntityFrameworkCore;

namespace WSSensorAguaData
{
    public class SensorContext : DbContext
    {
        public DbSet<Sensor> Sensors { get; set; }

        public SensorContext(DbContextOptions<SensorContext> options) : base(options)
        {
        }
    }
}