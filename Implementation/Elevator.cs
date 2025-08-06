using Interfaces;
using Models.DTOs;
using Models.Entities;
using Models.Enums;

namespace Implementation
{
    public class Elevator : IElevator
    {
        public async Task<ApiResponse<ElevatorStateDTO>> ElevatorUp(RequestElevatorDTO requestElevatorDTO)
        {
            if (requestElevatorDTO.CurrentFloor == 5)
            {
                return new ApiResponse<ElevatorStateDTO> { Success = false, Errors = new List<string> { "Yo cannot request upwards on the 5th floor" } };
            }

            if (requestElevatorDTO.CurrentFloor == requestElevatorDTO.FloorRequired)
            {
                return new ApiResponse<ElevatorStateDTO> { Success = false, Errors = new List<string> { "You are already on the requested floor" } };
            }

            return new ApiResponse<ElevatorStateDTO> 
            { 
                Success = true, 
                Data = new ElevatorStateDTO 
                { 
                    StateMovement = StateMovement.Moving,
                    CurrentFloor = requestElevatorDTO.FloorRequired,
                    CurrentDirection = DirectionElevator.Up,
                    Doors = StateDoor.Close
                } };
        }

        public async Task<ApiResponse<ElevatorStateDTO>> ElevatorDown(RequestElevatorDTO requestElevatorDTO)
        {
            if (requestElevatorDTO.CurrentFloor == 1)
            {
                return new ApiResponse<ElevatorStateDTO> { Success = false, Errors = new List<string> { "You cannot request a descent on the 1st floor" } };
            }

            if (requestElevatorDTO.CurrentFloor == requestElevatorDTO.FloorRequired)
            {
                return new ApiResponse<ElevatorStateDTO> { Success = false, Errors = new List<string> { "You are already on the requested floor" } };
            }

            return new ApiResponse<ElevatorStateDTO> 
            { 
                Success = true, 
                Data = new ElevatorStateDTO 
                { 
                    StateMovement = StateMovement.Moving,
                    CurrentFloor = requestElevatorDTO.FloorRequired,
                    CurrentDirection = DirectionElevator.Down,
                    Doors = StateDoor.Close
                } 
            };
        }

        public async Task<ApiResponse<ElevatorStateDTO>> RequestElevator(RequestElevatorDTO requestElevatorDTO)
        {
            return new ApiResponse<ElevatorStateDTO>
            {
                Success = true,
                Data = new ElevatorStateDTO
                {
                    CurrentFloor = requestElevatorDTO.CurrentFloor,
                    Doors = StateDoor.Open,
                    CurrentDirection = requestElevatorDTO.DirectionRequest,
                    StateMovement = StateMovement.Stop
                }
            };
        }
    }
}
