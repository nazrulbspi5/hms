using Autofac;
using AutoMapper;
using HMS.API.Models.RoomType;
using HMS.Infrastructure.Dtos;
using HMS.Infrastructure.Entities;
using HMS.Infrastructure.Services;

namespace HMS.API.Models.Room
{
    public class RoomModel : BaseModel
    {
        public Guid Id { get; set; }
        public string? RoomNo { get; set; }
        public Guid RoomTypeId { get; set; }
        public double Rate { get; set; }

        private IMapper? _mapper;
        private IRoomService _roomService;
        public RoomModel() : base() { }
        public RoomModel(IMapper mapper, IRoomService roomService)
        {
            _mapper = mapper;
            _roomService = roomService;
        }
        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _roomService = _scope.Resolve<IRoomService>();
            _mapper = _scope.Resolve<IMapper>();
        }
        public async Task CreateRoom()
        {
            var room = _mapper.Map<RoomDto>(this);
            await _roomService.CreateRoom(room);
        }
        public async Task EditRoom()
        {
            var roomType = _mapper.Map<RoomDto>(this);
            await _roomService.EditRoom(roomType, Id);
        }
        public async Task DeleteRoom(string roomId)
        {
            await _roomService.DeleteRoom(Guid.Parse(roomId));
        }
        public async Task<RoomModel> GetRoomById(string roomId)
        {
            var room = await _roomService.GetRoomById(Guid.Parse(roomId));
            var roomModel = _mapper.Map<RoomModel>(room);
            return roomModel;

        }
    }
}
