using Autofac;
using AutoMapper;
using HMS.API.ResponseModels.Room;
using HMS.Infrastructure.Dtos;
using HMS.Infrastructure.Services;
using System.Net;

namespace HMS.API.Models.Room
{
    public class RoomListModel : BaseModel
    {
        public IList<RoomModel> RoomModels { get; private set; }
        private IMapper _mapper;
        private IRoomService _roomService;

        public RoomListModel() : base()
        {

        }
        public RoomListModel(IMapper mapper, IRoomService roomService)
        {
            _mapper = mapper;
            _roomService = roomService;
            RoomModels = new List<RoomModel>();

        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _roomService = _scope.Resolve<IRoomService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public async Task<RoomResponse> GetAllRoomTypes()
        {
            var rooms = await _roomService.GetAllRooms();
            await AddRoomToList(rooms);
            RoomResponse response = new RoomResponse();
            if (rooms != null)
            {
                response.IsSuccess = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Result = RoomModels;
                response.Message = "All room get successfull";
            }
            else
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Result = null;
                response.Message = "We are unable to fetch your room";
            }
            return response;
        }
        private async Task AddRoomToList(IList<RoomDto> rooms)
        {
            foreach (var rType in rooms)
            {
                RoomModel roomModel = new RoomModel();
                roomModel = _mapper.Map(rType, roomModel);
                RoomModels.Add(roomModel);
            }

        }
    }
}
