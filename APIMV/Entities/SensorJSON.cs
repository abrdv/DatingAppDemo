namespace APIMV.Entities
{

    public class SensorJSON
    {
        public Provider[] providers { get; set; }
    }

    public class Provider
    {
        public string provider { get; set; }
        public string permission { get; set; }
        public Sensor[] sensors { get; set; }
    }

    public class Sensor
    {
        public string sensor { get; set; }
        public string description { get; set; }
        public string dataType { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public string unit { get; set; }
        public string timeZone { get; set; }
        public bool publicAccess { get; set; }
        public string component { get; set; }
        public string componentType { get; set; }
        public string componentDesc { get; set; }
        public bool componentPublicAccess { get; set; }
        public Additionalinfo additionalInfo { get; set; }
        public Componentadditionalinfo componentAdditionalInfo { get; set; }
        public Componenttechnicaldetails componentTechnicalDetails { get; set; }
        public Technicaldetails technicalDetails { get; set; }
    }

    public class Additionalinfo
    {
        public string Tempsmostreigmin { get; set; }
        public string Rangmínim { get; set; }
        public string Rangmàxim { get; set; }
    }

    public class Componentadditionalinfo
    {
        public string Comarca { get; set; }
        public string Província { get; set; }
        public string Riu { get; set; }
        public string Districtefluvial { get; set; }
        public string Subconca { get; set; }
        public string Termemunicipal { get; set; }
        public string Capacitatmàximaembassament { get; set; }
        public string Conca { get; set; }
        public string CoordenadaXUTMETRS89 { get; set; }
        public string CoordenadaYUTMETRS89 { get; set; }
        public string Superfícieconcadrenada { get; set; }
        public string Titular { get; set; }
        public string Estat { get; set; }
        public string CoordenadesXYUTMETRS89 { get; set; }
        public string Codiordre { get; set; }
        public string Tipologia { get; set; }
        public string Massadaigua { get; set; }
        public string Aqüífer { get; set; }
    }

    public class Componenttechnicaldetails
    {
        public string producer { get; set; }
        public string model { get; set; }
        public string serialNumber { get; set; }
        public string macAddress { get; set; }
        public string energy { get; set; }
        public string connectivity { get; set; }
    }

    public class Technicaldetails
    {
        public string producer { get; set; }
        public string model { get; set; }
        public string serialNumber { get; set; }
        public string energy { get; set; }
    }

}
