// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using Newtonsoft.Json;

public class AguaSensorJSON
{
    public List<Provider> providers { get; set; }
}

public class AdditionalInfo
{
    [JsonProperty("Temps mostreig (min)")]
    public string Tempsmostreigmin { get; set; }

    [JsonProperty("Rang mínim")]
    public string Rangmnim { get; set; }

    [JsonProperty("Rang màxim")]
    public string Rangmxim { get; set; }
}

public class ComponentAdditionalInfo
{
    public string Comarca { get; set; }
    public string Provncia { get; set; }
    public string Riu { get; set; }

    [JsonProperty("Districte fluvial")]
    public string Districtefluvial { get; set; }
    public string Subconca { get; set; }

    [JsonProperty("Terme municipal")]
    public string Termemunicipal { get; set; }

    [JsonProperty("Capacitat màxima embassament")]
    public string Capacitatmximaembassament { get; set; }
    public string Conca { get; set; }

    [JsonProperty("Coordenada X (UTM ETRS89)")]
    public string CoordenadaXUTMETRS89 { get; set; }

    [JsonProperty("Coordenada Y (UTM ETRS89)")]
    public string CoordenadaYUTMETRS89 { get; set; }

    [JsonProperty("Superfície conca drenada")]
    public string Superfcieconcadrenada { get; set; }
    public string Titular { get; set; }
    public string Estat { get; set; }

    [JsonProperty("Coordenades X,Y (UTM ETRS89)")]
    public string CoordenadesXYUTMETRS89 { get; set; }

    [JsonProperty("Codi ordre")]
    public string Codiordre { get; set; }
    public string Tipologia { get; set; }

    [JsonProperty("Massa d'aigua")]
    public string Massadaigua { get; set; }
    public string Aqfer { get; set; }
}

public class ComponentTechnicalDetails
{
    public string producer { get; set; }
    public string model { get; set; }
    public string serialNumber { get; set; }
    public string macAddress { get; set; }
    public string energy { get; set; }
    public string connectivity { get; set; }
}

public class Provider
{
    public string provider { get; set; }
    public string permission { get; set; }
    public List<Sensor> sensors { get; set; }
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
    public AdditionalInfo additionalInfo { get; set; }
    public ComponentAdditionalInfo componentAdditionalInfo { get; set; }
    public ComponentTechnicalDetails componentTechnicalDetails { get; set; }
    public TechnicalDetails technicalDetails { get; set; }
}

public class TechnicalDetails
{
    public string producer { get; set; }
    public string model { get; set; }
    public string serialNumber { get; set; }
    public string energy { get; set; }
}

