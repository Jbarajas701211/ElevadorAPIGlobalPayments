using Models.Enums;

namespace Models.DTOs
{
    public class ElevadorEstadoDTO
    {
        public int PisoActual { get; set; }
        public EstadoPuerta Puertas { get; set; }
        public EstadoMovimiento EstadoMovimiento { get; set; }
        public DireccionElevador DireccionActual { get; set; }
    }
}
