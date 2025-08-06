using Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;

namespace ElevadorAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ElevatorController : Controller
    {
        private readonly IElevator elevator;

        public ElevatorController(IElevator elevator)
        {
            this.elevator = elevator;
        }


        [HttpPost("up")]
        [ProducesResponseType(typeof(ApiResponse<StateElevator>), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<ApiResponse<ElevatorStateDTO>> Up(RequestElevatorDTO requestElevatorDTO)
        {
            return await elevator.ElevatorUp(requestElevatorDTO);
        }


        [HttpPost("requestElevator")]
        [ProducesResponseType(typeof(ApiResponse<StateElevator>), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<ApiResponse<ElevatorStateDTO>> RequestElevator(RequestElevatorDTO requestElevatorDTO)
        {
            return await elevator.RequestElevator(requestElevatorDTO);
        }

        [HttpPost("down")]
        [ProducesResponseType(typeof(ApiResponse<StateElevator>), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<ApiResponse<ElevatorStateDTO>> Down(RequestElevatorDTO requestElevatorDTO)
        {
            return await elevator.ElevatorDown(requestElevatorDTO);
        }
    }
}
