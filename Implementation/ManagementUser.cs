using Interfaces;
using Interfaces.Repository;
using Interfaces.Utility;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class ManagementUser : IManagementUser
    {
        private readonly IUserRepository _userRepository;
        private readonly IUtility _utility;

        public ManagementUser(IUserRepository userRepository, IUtility utility)
        {
            _userRepository = userRepository;
            _utility = utility;
        }

        public async Task<ApiResponse<ResponseAutenticationDTO>> UserRegistration(User user)
        {
            try
            {
                var existeUsuario = await ValidUser(user.Email ?? string.Empty);

                if (existeUsuario is not null)
                {
                    return new ApiResponse<ResponseAutenticationDTO>() { Success = false, Errors = new List<string>() { "The user already exists" } };
                }
                ;

                var seCreoUsuario = await _userRepository.CreateUserAsync(user);

                if (!seCreoUsuario)
                {
                    return new ApiResponse<ResponseAutenticationDTO>() { Success = false, Errors = new List<string>() { "The user could not be registered" } };
                }
                else
                {
                    var token = _utility.GenerateJWT(user);

                    return new ApiResponse<ResponseAutenticationDTO> { Success = true, Data = token };
                }


            }
            catch (Exception ex)
            {
                return new ApiResponse<ResponseAutenticationDTO> { Success = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<ApiResponse<ResponseAutenticationDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                var existUser = await ValidUser(loginDTO.Email);

                if (existUser is null)
                {
                    return new ApiResponse<ResponseAutenticationDTO> { Errors = new List<string> { "Unregistered user " } };
                }

                if (existUser.IsBlocked)
                {
                    return new ApiResponse<ResponseAutenticationDTO> { Errors = new List<string> { "Unregistered user " } };
                }

                var passwordValido = PasswordCorrecto(loginDTO.Password, existUser.Password!);

                if (!passwordValido)
                {
                    var attempsUser = await _userRepository.GetAttemptsUserAsync(existUser.IdUser);
                    if (attempsUser is null)
                    {
                        await _userRepository.CreateAttemptAsync(new UserAttempts { Attemps = 1, UserId = existUser.IdUser, Blocked = false });
                        return new ApiResponse<ResponseAutenticationDTO> { Errors = new List<string> { $"Just you have 2 attemps of 3 for introduce the correct password" } };
                    }

                    attempsUser!.Attemps += 1;

                    if (attempsUser.Attemps > 3)
                    {
                        attempsUser.Blocked = true;
                        attempsUser.DateBlocked = DateTime.Now;

                        var isBloqued = await _userRepository.UpdateAttemptAsync(attempsUser);

                        if (isBloqued)
                        {
                            existUser.IsBlocked = true;
                            isBloqued = await _userRepository.UpdateUserBlockedAsync(existUser);
                        }

                        return new ApiResponse<ResponseAutenticationDTO> { Errors = new List<string>() { "You user was blocked " } };
                    }

                    await _userRepository.UpdateAttemptAsync(attempsUser);

                    return new ApiResponse<ResponseAutenticationDTO> { Errors = new List<string> { $"Just you have {3 - attempsUser.Attemps} attemps of 3 for introduce the correct password" } };
                }

                var token = _utility.GenerateJWT(existUser);
                return new ApiResponse<ResponseAutenticationDTO> { Data = token, Success = true };
            }
            catch (Exception ex)
            {

                return new ApiResponse<ResponseAutenticationDTO> { Errors = new List<string> { ex.Message } };
            }
        }

        private bool PasswordCorrecto(string password, string passwordBd)
        {
            if (password == passwordBd)
            {
                return true;
            }
            return false;
        }

        private async Task<User?> ValidUser(string email)
        {
            return await _userRepository.GetUserForEmailAsync(email ?? string.Empty);
        }
    }
}
