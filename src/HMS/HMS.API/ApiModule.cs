using Autofac;
using HMS.API.Models.Auth;
using HMS.API.Models.Room;
using HMS.API.Models.RoomType;

namespace HMS.API
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<RoomModel>().AsSelf();
            builder.RegisterType<RoomListModel>().AsSelf();
            builder.RegisterType<RoomTypeListModel>().AsSelf();
            builder.RegisterType<RoomTypeModel>().AsSelf();

            base.Load(builder);
        }
    }
}
