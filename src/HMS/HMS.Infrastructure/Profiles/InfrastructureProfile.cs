using AutoMapper;
using HMS.Infrastructure.Dtos;
using HMS.Infrastructure.Dtos.Membership;
using HMS.Infrastructure.Entities;
using HMS.Infrastructure.Entities.Membership;


namespace HMS.Infrastructure.Profiles
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<ApplicationUserDto, ApplicationUser>()
                .ReverseMap();

            CreateMap<RoomType, RoomTypeDto>()
               .ReverseMap();

            CreateMap<Room, RoomDto>()
               .ReverseMap();
        }
    }
}