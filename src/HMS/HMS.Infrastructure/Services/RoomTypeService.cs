using AutoMapper;
using HMS.Infrastructure.Dtos;
using HMS.Infrastructure.Entities;
using HMS.Infrastructure.Exceptions;
using HMS.Infrastructure.Services.Membership;
using HMS.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HMS.Infrastructure.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoomTypeService(IApplicationUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateRoomType(RoomTypeDto roomType)
        {
          var count= _unitOfWork.RoomTypes.GetCount(x=>x.TypeName ==roomType.TypeName);
            if (count > 0)
                throw new DuplicateException("Room type already exists.");

            var roomTypeEntity = _mapper.Map<RoomType>(roomType);
            _unitOfWork.RoomTypes.Add(roomTypeEntity);
            _unitOfWork.Save();
        }

        public async Task DeleteRoomType(Guid roomTypeId)
        {
            var count = _unitOfWork.RoomTypes.GetCount(x=>x.Id == roomTypeId);
            if (count > 0)
            {
                _unitOfWork.RoomTypes.Remove(roomTypeId);
                _unitOfWork.Save();
            }                        
        }

        public async Task EditRoomType(RoomTypeDto roomType, Guid roomTypeId)
        {
            var roomTypeEntity = _mapper.Map<RoomType>(roomType);
            _unitOfWork.RoomTypes.Edit(roomTypeEntity);
            _unitOfWork.Save();
        }

        public async Task<IList<RoomTypeDto>> GetAllRoomTypes()
        {           
            var allRoomTypes= _unitOfWork.RoomTypes.GetAll();
            var roomTypeList = new List<RoomTypeDto>();
            foreach (var roomType in allRoomTypes)
            {
                var roomTypeDto = _mapper.Map<RoomTypeDto>(roomType);
                roomTypeList.Add(roomTypeDto);
            }
            return roomTypeList;           
        }
        public async Task<RoomTypeDto> GetRoomTypeById(Guid roomTypeId)
        {
            var roomTypeEntity = _unitOfWork.RoomTypes.GetById(roomTypeId);
            var roomTypeDto =  _mapper.Map<RoomTypeDto>(roomTypeEntity);
            return roomTypeDto;
        }
    }
}
