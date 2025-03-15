using Microsoft.EntityFrameworkCore;
namespace AguaSensorsJSON2DB.Entities
{
    public class AguaSensorData
    {
        public int Id { get; set; }
        public int AguaSensorDBId { get; set; }
        public string Provider { get; set; }
        public string Sensor { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }

}
