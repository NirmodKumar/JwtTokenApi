using System.Security.Claims;
using JwtTokenApi.Interfaces;
using JwtTokenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("token")]
        public IActionResult GenerateToken(LoginRequest loginRequest)
        {
            if (loginRequest is null)
            {
                return BadRequest("Invalid request!");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,loginRequest.UserName),
                new Claim(ClaimTypes.Email,loginRequest.Email),
                new Claim(ClaimTypes.Role,loginRequest.Role)
            };

            return Ok(new AuthenticationResponse(_tokenService.GenerateAccessToken(claims)));
        }
    }
}
