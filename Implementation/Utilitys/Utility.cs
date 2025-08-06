
using Interfaces.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Implementation.Utilitys
{
    public class Utility : IUtility
    {
        private readonly IConfiguration _configuration;

        public Utility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string EncryptSHA256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public ResponseAutenticationDTO GenerateJWT(User user)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.IdUser.ToString()),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var expiration = DateTime.UtcNow.AddMinutes(30);
            var tokenSecurity = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                signingCredentials: creds);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenSecurity);
            return new ResponseAutenticationDTO { Token = token, Expiration = expiration };
        }
    }
}
