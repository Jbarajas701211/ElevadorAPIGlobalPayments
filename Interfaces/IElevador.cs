using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IElevador
    {
        Task<ApiResponse<ElevadorEstadoDTO>> ElevadorHaciaArriba(SolicitudElevadorDTO solicitudElevadorDTO);
        Task<ApiResponse<ElevadorEstadoDTO>> ElevadorHaciaAbajo(SolicitudElevadorDTO solicitudElevadorDTO);
        Task<ApiResponse<ElevadorEstadoDTO>> LlamaElevador(SolicitudElevadorDTO solicitudElevadorDTO);
    }
}
