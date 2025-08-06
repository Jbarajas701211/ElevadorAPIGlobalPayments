using Models;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IManagementUser
    {
        Task<ApiResponse<ResponseAutenticationDTO>> UserRegistration(User user);

        Task<ApiResponse<ResponseAutenticationDTO>> Login(LoginDTO loginDTO);
    }
}
