using Newtonsoft.Json;

namespace Api.Responses.Marcadores;

public class ListadosMarcadores : ResponseBase
{
    [JsonProperty("litadoMarcadores")]
    public List<ItemMarcador> LitadoMarcadores { get; set; } = new List<ItemMarcador>();
    
}

public class ItemMarcador
{
    [JsonProperty("latitud")]
    public string Latitud { get; set; }
    [JsonProperty("longitud")]
    public string Longitud { get; set; }
    [JsonProperty("info")]
    public string Info { get; set; }
}