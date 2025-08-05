using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class SolicitudElevadorDTO : ElevadorEstadoDTO
    {
        [Required(ErrorMessage = "El piso solicitado es obligatorio")]
        public required int PisoSolicitado { get; set; }
        [Required(ErrorMessage = "La dirección del elevador es requerida")]
        public required DireccionElevador DireccionSolicitada { get; set; }
    }
}
