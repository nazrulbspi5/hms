using HMS.Infrastructure.Dtos.Memebership;

namespace HMS.Infrastructure.Services
{
    public interface IAccountService
    {
        Task<(bool isValidUser, UserInfoDto userInfo)> GetUserToken(string email, string password);

        Task<(bool isSuccess,string message)> Regiter(string name,string email, string password);
    }
}