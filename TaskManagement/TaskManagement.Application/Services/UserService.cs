using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Settings;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly JwtSettings _jwtSettings;

        public UserService(IRepository<UserEntity> userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<JwtSecurityToken> RegisterAsync(string userName, string password, string email)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new UserEntity {Name = userName, Password =  passwordHash, Email = email};

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();


            return GenerateJwtToken(user);
        }

        private JwtSecurityToken GenerateJwtToken(UserEntity user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );
        }
    }
}
