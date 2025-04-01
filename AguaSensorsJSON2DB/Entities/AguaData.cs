using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaSensorsJSON2DB.Entities
{
    public readonly record struct AguaData(
            int Id,
            int AguaSignalsId,
            string Provider,
            string Sensor,
            string Description,
            decimal Value,
            DateTime TimeStamp,
            DateTime Date
        );
}
