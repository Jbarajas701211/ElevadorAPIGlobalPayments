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
    public class ElevadorController : Controller
    {
        private readonly IElevador elevador;

        public ElevadorController(IElevador elevador)
        {
            this.elevador = elevador;
        }


        [HttpPost("subir")]
        [ProducesResponseType(typeof(ApiResponse<ElevadorEstado>), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<ApiResponse<ElevadorEstadoDTO>> Subir(SolicitudElevadorDTO solicitudElevadorDTO)
        {
            return await elevador.ElevadorHaciaArriba(solicitudElevadorDTO);
        }


        [HttpPost("llamaElevador")]
        [ProducesResponseType(typeof(ApiResponse<ElevadorEstado>), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<ApiResponse<ElevadorEstadoDTO>> LlamaElevador(SolicitudElevadorDTO solicitudElevadorDTO)
        {
            return await elevador.LlamaElevador(solicitudElevadorDTO);
        }

        [HttpPost("bajar")]
        [ProducesResponseType(typeof(ApiResponse<ElevadorEstado>), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<ApiResponse<ElevadorEstadoDTO>> Bajar(SolicitudElevadorDTO solicitudElevadorDTO)
        {
            return await elevador.ElevadorHaciaAbajo(solicitudElevadorDTO);
        }
    }
}
