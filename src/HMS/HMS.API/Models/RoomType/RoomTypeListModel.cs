using Autofac;
using AutoMapper;
using HMS.API.ResponseModels.RoomType;
using HMS.Infrastructure.Dtos;
using HMS.Infrastructure.Services;
using System.Net;

namespace HMS.API.Models.RoomType
{
    public class RoomTypeListModel : BaseModel
    {
        public IList<RoomTypeModel> RoomTypeModels { get; private set; }
        private IMapper _mapper;
        private IRoomTypeService _roomTypeService;

        public RoomTypeListModel() : base()
        {

        }
        public RoomTypeListModel(IMapper mapper, IRoomTypeService roomTypeService)
        {
            _mapper = mapper;
            _roomTypeService = roomTypeService;
            RoomTypeModels = new List<RoomTypeModel>();

        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _roomTypeService = _scope.Resolve<IRoomTypeService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public async Task<RoomTypeResponse> GetAllRoomTypes()
        {
            var roomTypes = await _roomTypeService.GetAllRoomTypes();
            await AddRoomTypeToList(roomTypes);
            RoomTypeResponse response = new RoomTypeResponse();
            if (roomTypes != null)
            {
                response.IsSuccess = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Result = RoomTypeModels;
                response.Message = "All room type get successfull";
            }
            else
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Result = null;
                response.Message = "We are unable to fetch your room type";
            }
            return response;
        }
        private async Task AddRoomTypeToList(IList<RoomTypeDto> roomTypes)
        {
            foreach (var rType in roomTypes)
            {
                RoomTypeModel roomTypeModel = new RoomTypeModel();
                roomTypeModel = _mapper.Map(rType, roomTypeModel);
                RoomTypeModels.Add(roomTypeModel);
            }

        }
    }
}
