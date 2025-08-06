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
    public class UserController : Controller
    {
        private readonly IManagementUser _managementUser;
        private readonly IMapper _mapper;
        private readonly IUtility _utility;

        public UserController(IMapper mapper, IManagementUser managementUser, IUtility utility)
        {
            _mapper = mapper;
            _managementUser = managementUser;
            _utility = utility;
        }

        [HttpPost("record", Name = "CreateUser")]
        public async Task<ApiResponse<ResponseAutenticationDTO>> Record(UserDTO userDTO)
        {
            userDTO.Password = _utility.EncryptSHA256(userDTO.Password);
            var user = _mapper.Map<User>(userDTO);

            try
            {
                return await _managementUser.UserRegistration(user);
                
            }
            catch (Exception ex)
            {
                return new ApiResponse<ResponseAutenticationDTO> { Success = false, Errors = new List<string> { ex.Message } };
            }
        }

        [HttpPost("login", Name = "Login")]
        public async Task<ApiResponse<ResponseAutenticationDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                loginDTO.Password = _utility.EncryptSHA256(loginDTO.Password);
                return await _managementUser.Login(loginDTO);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
