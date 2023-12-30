using AutoMapper;
using HMS.Infrastructure.Dtos;
using HMS.Infrastructure.Entities;
using HMS.Infrastructure.Exceptions;
using HMS.Infrastructure.UnitOfWorks;

namespace HMS.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoomService(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateRoom(RoomDto room)
        {
            var count = _unitOfWork.Rooms.GetCount(x => x.RoomNo == room.RoomNo);
            if (count > 0)
                throw new DuplicateException("Room no already exists.");

            var roomEntity = _mapper.Map<Room>(room);
            _unitOfWork.Rooms.Add(roomEntity);
            _unitOfWork.Save();
        }

        public async Task DeleteRoom(Guid roomId)
        {
           var count = _unitOfWork.Rooms.GetCount(x=>x.Equals(roomId));
            if (count > 0)
            {
                _unitOfWork.Rooms.Remove(roomId);
                _unitOfWork.Save();
            }
        }

        public async Task EditRoom(RoomDto room, Guid roomId)
        {
            var roomEntity = _mapper.Map<Room>(room);
            _unitOfWork.Rooms.Edit(roomEntity);
            _unitOfWork.Save();
        }

        public async Task<IList<RoomDto>> GetAllRooms()
        {
            var allRooms = _unitOfWork.Rooms.GetAll();
            var roomList = new List<RoomDto>();
            foreach (var room in allRooms)
            {
                var roomTypeDto = _mapper.Map<RoomDto>(room);
                roomList.Add(roomTypeDto);
            }
            return roomList;
        }

        public async Task<RoomDto> GetRoomById(Guid roomId)
        {
            var roomEntity = _unitOfWork.Rooms.GetById(roomId);
            var roomDto = _mapper.Map<RoomDto>(roomEntity);
            return roomDto;
        }
    }
}
