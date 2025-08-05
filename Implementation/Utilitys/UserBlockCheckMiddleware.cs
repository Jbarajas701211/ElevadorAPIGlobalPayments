using Interfaces;
using Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Implementation.Utilitys
{
    public class UserBlockCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserRepository _userRepository;

        public UserBlockCheckMiddleware(RequestDelegate next, IUserRepository userRepository)
        {
            _next = next;
            _userRepository = userRepository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var excludePaths = new[] { "/registro" };

            var path = context.Request.Path.Value.ToLower();

            if (excludePaths.Contains(path))
            {
                await _next(context);
                return;
            }

            if (context.User.Identity!.IsAuthenticated)
            {
                var userEmailClaim = context.User.FindFirst(ClaimTypes.Email);
                if(userEmailClaim is not null)
                {
                    var userEmail = userEmailClaim.Value;

                    bool isBlocked = await CheckIfUserIsBlockedAsync(userEmail);

                    if (isBlocked)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Usuario Bloqueado");
                        return;
                    }
                }
            }

            await _next(context);

            
        }

        private async Task<bool> CheckIfUserIsBlockedAsync(string email)
        {
            var userBd = await _userRepository.ObtenerUsuarioPorCorreoAsync(email);

            if (userBd is null)
            {
                return false;
            }

            return userBd.EsBloqueado;
        }
    }
}
