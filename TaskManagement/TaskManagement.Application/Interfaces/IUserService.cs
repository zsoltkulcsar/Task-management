using System.IdentityModel.Tokens.Jwt;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<JwtSecurityToken> RegisterAsync(string userName, string password, string email);
        Task<UserEntity?> FindUserByEmailAsync(string email);
        Task<bool> ValidatePasswordAsync(UserEntity user, string password);
        JwtSecurityToken GenerateJwtToken(UserEntity user);
    }
}