using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AguaSensorsJSON2DB.Entities
{
    public class AguaSensorDB
    {/*
      * CALC000041  Foix
      *  CALC000713 Baells (Cercs)
      *  CALC000152 Sau (Vilanova de Sau)
      *  CALC000046 Darnius Boadella (Darnius)
      *  CALC000168 Susqueda (Osor)
      *  CALC000735 Sant Ponç (Clariana de Cardener)
      *  CALC000722 Llosa del Cavall (Navès)
      *  CALC000125 Siurana (Cornudella de Montsant)
      *  CALC000120 Riudecanyes
      */
        public int Id { get; set; }
        public string Provider { get; set; }
        public string Sensor { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string ComponentType { get; set; }
        public string ComponentDesc { get; set; }
        public bool IsImportValue { get; set; }
    }
}
