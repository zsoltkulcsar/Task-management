using System.IdentityModel.Tokens.Jwt;

namespace TaskManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<JwtSecurityToken> RegisterAsync(string userName, string password, string email);
    }
}
