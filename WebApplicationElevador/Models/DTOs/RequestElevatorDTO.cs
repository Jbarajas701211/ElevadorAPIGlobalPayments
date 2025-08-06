using System.ComponentModel.DataAnnotations;
using WebApplicationElevador.Models.Enum;

namespace WebApplicationElevador.Models.DTOs
{
    public class RequestElevatorDTO : StateElevatorDTO
    {
        [Required(ErrorMessage = "The floor is required")]
        public required int FloorRequired { get; set; }
        [Required(ErrorMessage = "The direction of the elevator is required")]
        public required DirectionElevator DirectionRequest { get; set; }
    }
}
