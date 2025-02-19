using APIMV.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APIMV.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<SensorDTO> WaterSensors { get; set; }
    }
}
