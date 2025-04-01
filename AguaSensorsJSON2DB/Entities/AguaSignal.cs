using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaSensorsJSON2DB.Entities
{
    public readonly record struct AguaSignal(
        int Id,
        string Provider,
        string Sensor,
        string Description,
        string DataType,
        string Location,
        string Type,
        string ComponentType,
        string ComponentDesc,
        bool IsImportValue
    );

    

}
