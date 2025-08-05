using WebApplicationElevador.Models.Enum;

namespace WebApplicationElevador.Models
{
    public class ElevadorEstado
    {
        public int PisoActual { get; set; }
        public EstadoPuerta Puertas { get; set; }
        public EstadoMovimiento Estado { get; set; }
        public DireccionElevador DireccionActual { get; set; }

        public ElevadorEstado()
        {
            this.PisoActual = 1;
            this.Puertas = EstadoPuerta.Cerrada;
            this.Estado = EstadoMovimiento.Parado;
            this.DireccionActual = DireccionElevador.Ninguna;
        }
    }
}
