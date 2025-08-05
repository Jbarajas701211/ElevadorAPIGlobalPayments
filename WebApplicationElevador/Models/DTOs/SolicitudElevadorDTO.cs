using System.ComponentModel.DataAnnotations;
using WebApplicationElevador.Models.Enum;

namespace WebApplicationElevador.Models.DTOs
{
    public class SolicitudElevadorDTO : ElevadorEstadoDTO
    {
        [Required(ErrorMessage = "El piso solicitado es obligatorio")]
        public required int PisoSolicitado { get; set; }
        [Required(ErrorMessage = "La dirección del elevador es requerida")]
        public required DireccionElevador DireccionSolicitada { get; set; }
    }
}
