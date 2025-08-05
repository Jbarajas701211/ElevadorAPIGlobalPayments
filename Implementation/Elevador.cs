using Interfaces;
using Models.DTOs;
using Models.Entities;
using Models.Enums;

namespace Implementation
{
    public class Elevador : IElevador
    {
        public async Task<ApiResponse<ElevadorEstadoDTO>> ElevadorHaciaArriba(SolicitudElevadorDTO solicitudElevadorDTO)
        {
            if (solicitudElevadorDTO.PisoActual == 5)
            {
                return new ApiResponse<ElevadorEstadoDTO> { Success = false, Errors = new List<string> { "No se puede solicitar hacia arriba en el piso 5" } };
            }

            if (solicitudElevadorDTO.PisoActual == solicitudElevadorDTO.PisoSolicitado)
            {
                return new ApiResponse<ElevadorEstadoDTO> { Success = false, Errors = new List<string> { "Ya se encuentra en el piso solicitado" } };
            }

            return new ApiResponse<ElevadorEstadoDTO> 
            { 
                Success = true, 
                Data = new ElevadorEstadoDTO 
                { 
                    EstadoMovimiento = EstadoMovimiento.Moviendo,
                    PisoActual = solicitudElevadorDTO.PisoSolicitado,
                    DireccionActual = DireccionElevador.Subir,
                    Puertas = EstadoPuerta.Cerrada
                } };
        }

        public async Task<ApiResponse<ElevadorEstadoDTO>> ElevadorHaciaAbajo(SolicitudElevadorDTO solicitudElevadorDTO)
        {
            if (solicitudElevadorDTO.PisoActual == 1)
            {
                return new ApiResponse<ElevadorEstadoDTO> { Success = false, Errors = new List<string> { "No se puede solicitar hacia abajo en el piso 1" } };
            }

            if (solicitudElevadorDTO.PisoActual == solicitudElevadorDTO.PisoSolicitado)
            {
                return new ApiResponse<ElevadorEstadoDTO> { Success = false, Errors = new List<string> { "Ya se encuentra en el piso solicitado" } };
            }

            return new ApiResponse<ElevadorEstadoDTO> 
            { 
                Success = true, 
                Data = new ElevadorEstadoDTO 
                { 
                    EstadoMovimiento = EstadoMovimiento.Moviendo,
                    PisoActual = solicitudElevadorDTO.PisoSolicitado,
                    DireccionActual = DireccionElevador.Bajar,
                    Puertas = EstadoPuerta.Cerrada
                } 
            };
        }

        public async Task<ApiResponse<ElevadorEstadoDTO>> LlamaElevador(SolicitudElevadorDTO solicitudElevadorDTO)
        {
            return new ApiResponse<ElevadorEstadoDTO>
            {
                Success = true,
                Data = new ElevadorEstadoDTO
                {
                    PisoActual = solicitudElevadorDTO.PisoActual,
                    Puertas = EstadoPuerta.Abierta,
                    DireccionActual = solicitudElevadorDTO.DireccionSolicitada,
                    EstadoMovimiento = EstadoMovimiento.Parado
                }
            };
        }
    }
}
