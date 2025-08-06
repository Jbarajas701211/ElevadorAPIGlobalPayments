using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationElevador.Models.Enum;

namespace WebApplicationElevador.Models.View
{
    public class ModelElevatorView
    {
        public StateElevator StateElevator { get; set; }
        public StateDoor StateDoor { get; set; }
        public StateMovement StateMovement { get; set; }
        public DirectionElevator DirectionElevator { get; set; }

        public IEnumerable<SelectListItem> StateDoorOptions { get; set; }
        public IEnumerable<SelectListItem> StateMovementOptions { get; set; }
        public IEnumerable<SelectListItem> DirectionOptions { get; set; }

        public ModelElevatorView()
        {
            StateElevator = new StateElevator()
            {
                CurrentFloor = 1,
                Doors = StateDoor.Close,
                StateMovement = StateMovement.Stop,
                CurrentDirection = DirectionElevator.None
            };
            StateDoor = StateDoor.Close;
            StateMovement = StateMovement.Stop;
            DirectionElevator = DirectionElevator.None;
        }

    }
}
