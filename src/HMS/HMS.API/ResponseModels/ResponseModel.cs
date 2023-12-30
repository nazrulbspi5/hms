using HMS.API.Models;

namespace HMS.API.ResponseModels
{
    public class ResponseModel<T>
    {
        public T? Result { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string[]? Errors { get; set; }
    }
}
