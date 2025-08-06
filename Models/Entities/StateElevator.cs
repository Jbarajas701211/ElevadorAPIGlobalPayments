using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class StateElevator
    {
        public int CurrentFloor { get; set; }
        public StateDoor Doors { get; set; }
        public StateMovement State {  get; set; }
        public DirectionElevator CurrentDirection { get; set; }

    }
}
