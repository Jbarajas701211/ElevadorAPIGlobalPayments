using AutoMapper;
using Interfaces;
using Interfaces.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;

namespace ElevadorAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IManagementUsuario _managementUsuario;
        private readonly IMapper _mapper;
        private readonly IUtility _utility;

        public UsuarioController(IMapper mapper, IManagementUsuario managementUsuario, IUtility utility)
        {
            _mapper = mapper;
            _managementUsuario = managementUsuario;
            _utility = utility;
        }

        [HttpPost("registro", Name = "CrearUsuario")]
        public async Task<ApiResponse<RespuestaAutenticacionDTO>> Registrarse(UsuarioDTO usuarioDTO)
        {
            usuarioDTO.Clave = _utility.encriptarSHA256(usuarioDTO.Clave);
            var usuario = _mapper.Map<Usuario>(usuarioDTO);

            try
            {
                return await _managementUsuario.RegistroUsuario(usuario);
                
            }
            catch (Exception ex)
            {
                return new ApiResponse<RespuestaAutenticacionDTO> { Success = false, Errors = new List<string> { ex.Message } };
            }
        }

        [HttpPost("login", Name = "Login")]
        public async Task<ApiResponse<RespuestaAutenticacionDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                loginDTO.Clave = _utility.encriptarSHA256(loginDTO.Clave);
                return await _managementUsuario.Login(loginDTO);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
