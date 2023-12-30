using HMS.API.Models.RoomType;

namespace HMS.API.ResponseModels.RoomType
{
    public class RoomTypeResponse
    {
        public IList<RoomTypeModel>? Result { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
