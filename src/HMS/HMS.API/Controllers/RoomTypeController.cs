using Autofac;
using HMS.API.Models.RoomType;
using HMS.API.ResponseModels;
using HMS.API.ResponseModels.RoomType;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace HMS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly ILogger<RoomTypeController> _logger;
        private readonly ILifetimeScope _scope;

        public RoomTypeController(ILogger<RoomTypeController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
            _logger.LogInformation("RoomType controller are calling");
        }
        [HttpPost("CreateRoomType")]
        public async Task<ResponseModel<RoomTypeModel>> Post([FromBody] RoomTypeModel roomTypeModel)
        {
            var responseModel = new ResponseModel<RoomTypeModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    roomTypeModel.ResolveDependency(_scope);
                    await roomTypeModel.CreateRoomType();
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Room type save successfully!";
                    responseModel.StatusCode = (int)HttpStatusCode.OK;
                    responseModel.Errors = new string[] { };
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);

                    responseModel.IsSuccess = true;
                    responseModel.Message = "Room type save failed!";
                    responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = new string[] { e.Message };
                }
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                responseModel.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
            }

            return responseModel;
        }
        [HttpPut("EditRoomType")]
        public async Task<ResponseModel<RoomTypeModel>> Put([FromBody] RoomTypeModel roomTypeModel)
        {
            var responseModel = new ResponseModel<RoomTypeModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    roomTypeModel.ResolveDependency(_scope);
                    await roomTypeModel.EditRoomType();
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Room type update successfully!";
                    responseModel.StatusCode = (int)HttpStatusCode.OK;
                    responseModel.Errors = new string[] { };
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);

                    responseModel.IsSuccess = true;
                    responseModel.Message = "Room type update failed!";
                    responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = new string[] { e.Message };
                }
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                responseModel.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
            }

            return responseModel;
        }

        [HttpGet("GetAllRoomTypes")]
        public async Task<RoomTypeResponse> Get()
        {
            var roomTypeListModel = _scope.Resolve<RoomTypeListModel>();
            //var id = Guid.Parse(User.FindFirstValue(ClaimTypes.Sid));
            RoomTypeResponse response = await roomTypeListModel.GetAllRoomTypes();

            return response;
        }
        [HttpDelete("DeleteRoomType")]
        public async Task<ResponseModel<RoomTypeModel>> Delete(string roomTypeId)
        {
            var responseModel = new ResponseModel<RoomTypeModel>();
            try
            {
                var roomTypeModel = _scope.Resolve<RoomTypeModel>();
                await roomTypeModel.DeleteRoomType(roomTypeId);

                responseModel.IsSuccess = true;
                responseModel.Message = "Room type delete successfully";
                responseModel.StatusCode = (int)HttpStatusCode.OK;
                responseModel.Errors = new string[] { };
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                responseModel.IsSuccess = true;
                responseModel.Message = "Room type delete failed!";
                responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                responseModel.Errors = new string[] { e.Message };
            }
            return responseModel;
        }
        [HttpGet("GetRoomTypeById")]
        public async Task<ResponseModel<RoomTypeModel>> GetRoomTypeById(string roomTypeId)
        {
            var responseModel = new ResponseModel<RoomTypeModel>();
            try
            {
                var roomTypeModel = _scope.Resolve<RoomTypeModel>();
                var roomType = await roomTypeModel.GetRoomTypeById(roomTypeId);
                responseModel.IsSuccess = true;
                responseModel.Result = roomType;
                responseModel.Message = "Room type fetch successfully";
                responseModel.StatusCode = (int)HttpStatusCode.OK;
                responseModel.Errors = new string[] { };
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                responseModel.IsSuccess = true;
                responseModel.Message = "Room type fetch failed!";
                responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                responseModel.Errors = new string[] { e.Message };
            }
            return responseModel;
        }
    }
}
