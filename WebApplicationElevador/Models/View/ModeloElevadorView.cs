using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationElevador.Models.Enum;

namespace WebApplicationElevador.Models.View
{
    public class ModeloElevadorView
    {
        public ElevadorEstado ElevadorEstado { get; set; }
        public EstadoPuerta EstadoPuerta { get; set; }
        public EstadoMovimiento EstadoMovimiento { get; set; }
        public DireccionElevador DireccionElevador { get; set; }

        public IEnumerable<SelectListItem> EstadoPuertaOptions { get; set; }
        public IEnumerable<SelectListItem> EstadoMovimientoOptions { get; set; }
        public IEnumerable<SelectListItem> DireccionOptions { get; set; }

        public ModeloElevadorView()
        {
            ElevadorEstado = new ElevadorEstado()
            {
                PisoActual = 1,
                Puertas = EstadoPuerta.Cerrada,
                Estado = EstadoMovimiento.Parado,
                DireccionActual = DireccionElevador.Ninguna
            };
            EstadoPuerta = EstadoPuerta.Cerrada;
            EstadoMovimiento = EstadoMovimiento.Parado;
            DireccionElevador = DireccionElevador.Ninguna;
        }

    }
}
