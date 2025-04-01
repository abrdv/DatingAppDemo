using APIMV.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIMV.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<AguaSensorData> AguaSensorData { get; set; }
    }
}
