using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtTokenApi.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace JwtTokenApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {

            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = Encoding.UTF8.GetBytes(_configuration["Auth:SecretKey"]);

            var symetricSecurityKey = new SymmetricSecurityKey(secretKey);

            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Auth:Issuer"],
                audience: _configuration["Auth:Audience"],
                signingCredentials: signingCredentials,
                claims: claims,
                expires: DateTime.Now.AddMinutes(5));

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
