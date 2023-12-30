using System.Security.Claims;

namespace HMS.Infrastructure.Services
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
        Task<(string token, DateTime expireDate)> GetJwtTokenWithExpireDate(IList<Claim> claims);
    }
}