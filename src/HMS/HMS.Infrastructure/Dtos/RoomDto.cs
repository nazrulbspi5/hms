using HMS.Infrastructure.Entities;

namespace HMS.Infrastructure.Dtos
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string? RoomNo { get; set; }
        public Guid RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        public double Rate { get; set; }
    }
}
