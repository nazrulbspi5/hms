using HMS.API.Models.Room;

namespace HMS.API.ResponseModels.Room
{
    public class RoomResponse
    {
        public IList<RoomModel>? Result { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
