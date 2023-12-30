using HMS.Infrastructure.Dtos;

namespace HMS.Infrastructure.Services
{
    public interface IRoomTypeService
    {
        Task CreateRoomType(RoomTypeDto roomType);
        Task EditRoomType(RoomTypeDto roomType,Guid roomTypeId);
        Task<RoomTypeDto> GetRoomTypeById(Guid roomTypeId);
        Task<IList<RoomTypeDto>> GetAllRoomTypes();
        Task DeleteRoomType(Guid roomTypeId);
    }
}
