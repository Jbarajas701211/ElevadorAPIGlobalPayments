using System.Text.Json.Serialization;
using WebApplicationElevador.Models.Enum;

namespace WebApplicationElevador.Models.DTOs
{
    public class ElevadorEstadoDTO
    {
        [JsonPropertyName("pisoActual")]
        public int PisoActual { get; set; }
        [JsonPropertyName("puertas")]
        public EstadoPuerta Puertas { get; set; }
        [JsonPropertyName("estadoMovimiento")]
        public EstadoMovimiento EstadoMovimiento { get; set; }
        [JsonPropertyName("direccionActual")]
        public DireccionElevador DireccionActual { get; set; }
    }
}
