using WebApplicationElevador.Models.Enum;

namespace WebApplicationElevador.Models
{
    public class StateElevator
    {
        public int CurrentFloor { get; set; }
        public StateDoor Doors { get; set; }
        public StateMovement StateMovement { get; set; }
        public DirectionElevator CurrentDirection { get; set; }

        public StateElevator()
        {
            this.CurrentFloor = 1;
            this.Doors = StateDoor.Close;
            this.StateMovement = StateMovement.Stop;
            this.CurrentDirection = DirectionElevator.None;
        }
    }
}
