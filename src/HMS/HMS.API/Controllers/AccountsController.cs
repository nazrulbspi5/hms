using Autofac;
using HMS.API.Models.Auth;
using HMS.API.ResponseModels;
using HMS.API.ResponseModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HMS.API.Controllers
{
    [Route("api/v1/[controller]")]
    //[EnableCors("AllowSites")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly ILifetimeScope _scope;

        public AccountsController(ILogger<AccountsController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        [HttpPost("Login")]
        public async Task<LoginResponseModel> Login([FromBody] LoginModel model)
        {
            LoginResponseModel response = new LoginResponseModel();

            if (ModelState.IsValid)
            {
                try
                {
                    model.ResolveDependency(_scope);
                    response = await model.GetUserToken();
                    response.Message = "Login successfull";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);

                    response.IsSuccess = false;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Result = null;
                    response.Message = "Login failed!";
                    response.Errors = new string[] { "Login failed!" };
                }
            }
            else
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Result = null;
                response.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
            }

            return response;
        }
        [HttpPost("Register")]
        public async Task<ResponseModel<RegisterModel>> Register([FromBody] RegisterModel model)
        {
            ResponseModel<RegisterModel> response = new ResponseModel<RegisterModel>();
            if (ModelState.IsValid)
            {
                try
                {
                    model.ResolveDependency(_scope);
                    response = await model.Register();
                    _logger.LogInformation("User created a new account with password.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    response.IsSuccess = false;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Errors = new string[] { "Registration failed!" };
                    response.Message =  "Registration failed!" ;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Result = null;
                response.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
            }

            return response;
        }
        //[HttpPost("Logout")]
        //public async Task<IActionResult> Logout(string? returnUrl = null)
        //{
        //    await _signInManager.SignOutAsync();

        //    return RedirectToAction(nameof(Login));
        //}
        //[Authorize]
        //[HttpPut(nameof(ChangePassword))]
        //public async Task<ActionResult> ChangePassword(ChangePasswordRequestModel model)
        // => await this.identity
        //     .ChangePasswordAsync(model, this.currentUser.UserId)
        //     .ToActionResult();

    }
}