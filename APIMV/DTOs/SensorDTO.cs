namespace APIMV.DTOs
{
    /*
         "provider": "EMBASSAMENT-EST",
         "permission": "READ",
         "sensor": "CALC000041",
         "description": "Percentatge volum embassat",
         "dataType": "NUMBER",
         "location": "41.255617318 1.651034658",
         "type": "0039",
         "unit": "%",
         "component": "080581-002",
         "componentType": "embassament",
         "componentDesc": "Foix (Castellet i la Gornal)",
         "Temps mostreig (min)": "5",
         "Rang mínim": "0",
         "Rang màxim": "110"
         "Comarca": "ALT PENEDÈS",
         "Província": "BARCELONA",
         "Subconca": "EL FOIX",
         "Capacitat màxima embassament": "3,74 hm³",
         "Conca": "EL FOIX",
         "Superfície conca drenada": "293,32 km²",

         */
    public record SensorDTO(
        Guid Id, 
        bool useStat, 
        string? provider, 
        string? permission, 
        string? sensor, 
        string? description, 
        string? dataType, 
        string? location, 
        string? type, 
        string? unit, 
        string? component, 
        string? componentType, 
        string? componentDesc, 
        string? Tempsmostreigmin, 
        string? Rangmínim, 
        string? Rangmàxim, 
        string? Comarca, 
        string? Província, 
        string? Subconca, 
        string? Capacitatmàximaembassament, 
        string? Conca, 
        string? Superfícieconcadrenada
        );
}
