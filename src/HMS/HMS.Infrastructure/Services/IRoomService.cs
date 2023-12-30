using HMS.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Infrastructure.Services
{
    public interface IRoomService
    {
        Task CreateRoom(RoomDto room);
        Task EditRoom(RoomDto roomType, Guid roomId);
        Task<RoomDto> GetRoomById(Guid roomId);
        Task<IList<RoomDto>> GetAllRooms();
        Task DeleteRoom(Guid roomId);
    }
}
