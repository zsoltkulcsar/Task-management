using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (string.IsNullOrEmpty(registerDto.Name) || string.IsNullOrEmpty(registerDto.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            var token = await _userService.RegisterAsync(registerDto.Name, registerDto.Password, registerDto.Email);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.FindUserByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized("Invalid credentials");

            var isPasswordValid = await _userService.ValidatePasswordAsync(user, loginDto.Password);
            if (!isPasswordValid) return Unauthorized("Invalid credentials");

            var token = _userService.GenerateJwtToken(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}