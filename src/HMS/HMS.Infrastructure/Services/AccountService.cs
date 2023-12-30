using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using HMS.Infrastructure.Entities.Membership;
using HMS.Infrastructure.Dtos.Memebership;

namespace HMS.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<(bool isValidUser, UserInfoDto? userInfo)> GetUserToken(string email, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);

            if (applicationUser != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(applicationUser, password, true);

                if (result != null && result.Succeeded)
                {
                    var claims = (await _userManager.GetClaimsAsync(applicationUser)).ToList();
                    claims.Add(new Claim(ClaimTypes.Sid, applicationUser.Id.ToString()));
                    var jwtToken = await _tokenService.GetJwtTokenWithExpireDate(claims);

                    UserInfoDto userInfo = new UserInfoDto();
                    userInfo.UserId = applicationUser.Id;
                    userInfo.Email = applicationUser.Email;
                    userInfo.Name = applicationUser.Name;
                    userInfo.Token = jwtToken.token;
                    userInfo.ExpireDate = jwtToken.expireDate;

                    return (true, userInfo);
                }
            }

            return (false, null);
        }

        public async Task<(bool isSuccess, string message)> Regiter(string name, string email, string password)
        {
            var applicationUser = await _userManager.FindByNameAsync(email);

            if (applicationUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Name = name,
                };
               var result= await _userManager.CreateAsync(user, password);
                if(result.Succeeded)
                    return (true, "Registration Successfully!");
            }
            else
            {
                return (false, "UserName already exist!");
            }
            return (false, "");
        }
    }
}