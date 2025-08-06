using Models.Enums;

namespace Models.DTOs
{
    public class ElevatorStateDTO
    {
        public int CurrentFloor { get; set; }
        public StateDoor Doors { get; set; }
        public StateMovement StateMovement { get; set; }
        public DirectionElevator CurrentDirection { get; set; }
    }
}
