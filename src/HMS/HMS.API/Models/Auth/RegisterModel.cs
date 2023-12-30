using Autofac;
using AutoMapper;
using HMS.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;
using HMS.API.ResponseModels;

namespace HMS.API.Models.Auth
{
    public class RegisterModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        private IAccountService _accountService;
        private IMapper _mapper;

        public RegisterModel() : base() { }

        public RegisterModel(IMapper mapper, IAccountService accountService)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _mapper = _scope.Resolve<IMapper>();
            _accountService = _scope.Resolve<IAccountService>();
        }

        internal async Task<ResponseModel<RegisterModel>> Register()
        {
            var model = new ResponseModel<RegisterModel>();
            var result = await _accountService.Regiter(Name, Email, Password);

            if (result.isSuccess)
            {
                model.IsSuccess = true;
                model.StatusCode = (int)HttpStatusCode.OK;
                model.Message = result.message;
                model.Result = null;
                model.Errors = new string[] { result.message };
            }
            else
            {
                model.IsSuccess = false;
                model.Message = result.message;
                model.Result = null;
                model.StatusCode = (int)HttpStatusCode.BadRequest;
                model.Errors = new string[] { "Registration failed" };
            }

            return model;
        }
    }
}