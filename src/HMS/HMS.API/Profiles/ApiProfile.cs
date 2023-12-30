using AutoMapper;
using HMS.API.Models.Auth;
using HMS.API.Models.Room;
using HMS.API.Models.RoomType;
using HMS.Infrastructure.Dtos;
using HMS.Infrastructure.Dtos.Memebership;

namespace HMS.API.Profiles
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<UserInfoDto, UserInfo>();
            CreateMap<RoomTypeModel, RoomTypeDto>()
               .ReverseMap();

            CreateMap<RoomModel, RoomDto>()
               .ReverseMap();
        }
    }
}