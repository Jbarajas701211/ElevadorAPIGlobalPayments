using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IElevator
    {
        Task<ApiResponse<ElevatorStateDTO>> ElevatorUp(RequestElevatorDTO requestElevatorDTO);
        Task<ApiResponse<ElevatorStateDTO>> ElevatorDown(RequestElevatorDTO requestElevatorDTO);
        Task<ApiResponse<ElevatorStateDTO>> RequestElevator(RequestElevatorDTO requestElevatorDTO);
    }
}
