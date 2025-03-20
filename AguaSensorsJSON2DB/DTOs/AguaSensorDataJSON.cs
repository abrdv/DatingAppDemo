
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AguaSensorsJSON2DB.DTOs
{
    // AguaSensorDataJSON myDeserializedClass = JsonConvert.DeserializeObject<AguaSensorDataJSON>(myJsonResponse);
    public class AguaSensorDataJSON
    {
        public List<Observation> observations { get; set; }
    }

    public class Observation
    {
        public decimal value { get; set; }
        public string timestamp { get; set; }
        public string location { get; set; }
    }

}
