using Autofac;
using AutoMapper;
using HMS.Infrastructure.Dtos;
using HMS.Infrastructure.Services;

namespace HMS.API.Models.RoomType
{
    public class RoomTypeModel : BaseModel
    {
        public Guid Id { get; set; }
        public string? TypeName { get; set; }

        private IMapper _mapper;
        private IRoomTypeService _roomTypeService;
        public RoomTypeModel() : base() { }
        public RoomTypeModel(IMapper mapper, IRoomTypeService roomTypeService)
        {
            _mapper = mapper;
            _roomTypeService = roomTypeService;
        }
        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _roomTypeService = _scope.Resolve<IRoomTypeService>();
            _mapper = _scope.Resolve<IMapper>();
        }
        public async Task CreateRoomType()
        {
            var roomType = _mapper.Map<RoomTypeDto>(this);
            await _roomTypeService.CreateRoomType(roomType);
        }
        public async Task EditRoomType()
        {
            var roomType = _mapper.Map<RoomTypeDto>(this);
            await _roomTypeService.EditRoomType(roomType, Id);
        }
        public async Task DeleteRoomType(string roomTypeId)
        {
            await _roomTypeService.DeleteRoomType(Guid.Parse(roomTypeId));
        }
        public async Task<RoomTypeModel> GetRoomTypeById(string roomTypeId)
        {
            var roomType = await _roomTypeService.GetRoomTypeById(Guid.Parse(roomTypeId));
            var roomTypeModel = _mapper.Map<RoomTypeModel>(roomType);
            return roomTypeModel;

        }

    }
}
