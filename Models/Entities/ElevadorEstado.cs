using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class ElevadorEstado
    {
        public int PisoActual { get; set; }
        public EstadoPuerta Puertas { get; set; }
        public EstadoMovimiento Estado {  get; set; }
        public DireccionElevador DireccionActual { get; set; }

    }
}
