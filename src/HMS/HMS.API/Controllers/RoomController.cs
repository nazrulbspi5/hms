using Autofac;
using HMS.API.Models.Room;
using HMS.API.ResponseModels;
using HMS.API.ResponseModels.Room;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly ILifetimeScope _scope;

        public RoomController(ILogger<RoomController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
            _logger.LogInformation("Room controller are calling");
        }

        [HttpPost("CreateRoom")]
        public async Task<ResponseModel<RoomModel>> Post([FromBody] RoomModel roomModel)
        {
            var responseModel = new ResponseModel<RoomModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    roomModel.ResolveDependency(_scope);
                    await roomModel.CreateRoom();
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Room save successfully!";
                    responseModel.StatusCode = (int)HttpStatusCode.OK;
                    responseModel.Errors = new string[] { };
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);

                    responseModel.IsSuccess = true;
                    responseModel.Message = "Room save failed!";
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

        [HttpPut("EditRoom")]
        public async Task<ResponseModel<RoomModel>> Put([FromBody] RoomModel roomModel)
        {
            var responseModel = new ResponseModel<RoomModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    roomModel.ResolveDependency(_scope);
                    await roomModel.EditRoom();
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Room update successfully!";
                    responseModel.StatusCode = (int)HttpStatusCode.OK;
                    responseModel.Errors = new string[] { };
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);

                    responseModel.IsSuccess = true;
                    responseModel.Message = "Room update failed!";
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

        [HttpGet("GetAllRooms")]
        public async Task<RoomResponse> Get()
        {
            var roomListModel = _scope.Resolve<RoomListModel>();          
            RoomResponse response = await roomListModel.GetAllRoomTypes();

            return response;
        }
        [HttpDelete("DeleteRoom")]
        public async Task<ResponseModel<RoomModel>> Delete(string roomId)
        {
            var responseModel = new ResponseModel<RoomModel>();
            try
            {
                var roomModel = _scope.Resolve<RoomModel>();
                await roomModel.DeleteRoom(roomId);

                responseModel.IsSuccess = true;
                responseModel.Message = "Room delete successfully";
                responseModel.StatusCode = (int)HttpStatusCode.OK;
                responseModel.Errors = new string[] { };
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                responseModel.IsSuccess = true;
                responseModel.Message = "Room delete failed!";
                responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                responseModel.Errors = new string[] { e.Message };
            }
            return responseModel;
        }
        [HttpGet("GetRoomById")]
        public async Task<ResponseModel<RoomModel>> GetRoomById(string roomId)
        {
            var responseModel = new ResponseModel<RoomModel>();
            try
            {
                var roomModel = _scope.Resolve<RoomModel>();
                var room = await roomModel.GetRoomById(roomId);
                responseModel.IsSuccess = true;
                responseModel.Result = room;
                responseModel.Message = "Room fetch successfully";
                responseModel.StatusCode = (int)HttpStatusCode.OK;
                responseModel.Errors = new string[] { };
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                responseModel.IsSuccess = true;
                responseModel.Message = "Room fetch failed!";
                responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                responseModel.Errors = new string[] { e.Message };
            }
            return responseModel;
        }

    }
}
