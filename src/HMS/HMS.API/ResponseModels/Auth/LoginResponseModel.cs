using HMS.API.Models.Auth;

namespace HMS.API.ResponseModels.Auth
{
    public class LoginResponseModel
    {
        public UserInfo? Result { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string[]? Errors { get; set; }
    }
}