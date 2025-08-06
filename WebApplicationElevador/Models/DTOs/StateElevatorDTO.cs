using System.Text.Json.Serialization;
using WebApplicationElevador.Models.Enum;

namespace WebApplicationElevador.Models.DTOs
{
    public class StateElevatorDTO
    {
        [JsonPropertyName("currentFloor")]
        public int CurrentFloor { get; set; }
        [JsonPropertyName("doors")]
        public StateDoor Doors { get; set; }
        [JsonPropertyName("stateMovement")]
        public StateMovement StateMovement { get; set; }
        [JsonPropertyName("currentDirection")]
        public DirectionElevator CurrentDirection { get; set; }
    }
}
