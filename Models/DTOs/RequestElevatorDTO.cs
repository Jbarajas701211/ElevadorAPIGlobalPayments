using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RequestElevatorDTO : ElevatorStateDTO
    {
        [Required(ErrorMessage = "The floor is required")]
        public required int FloorRequired { get; set; }
        [Required(ErrorMessage = "The direction of the elevator is required")]
        public required DirectionElevator DirectionRequest { get; set; }
    }
}
