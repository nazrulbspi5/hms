using Autofac;
using HMS.Infrastructure.DbContexts;
using HMS.Infrastructure.Repositories;
using HMS.Infrastructure.Services;
using HMS.Infrastructure.Services.Membership;
using HMS.Infrastructure.UnitOfWorks;

namespace HMS.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public InfrastructureModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            {
                builder.RegisterType<ApplicationDbContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssemblyName", _migrationAssemblyName)
               .InstancePerLifetimeScope();

                builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                    .WithParameter("connectionString", _connectionString)
                    .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                    .InstancePerLifetimeScope();

                builder.RegisterType<ApplicationUnitOfWork>()
                    .As<IApplicationUnitOfWork>()
                    .InstancePerLifetimeScope();


                builder.RegisterType<ApplicationUserManager>().AsSelf();

                builder.RegisterType<ApplicationSignInManager>().AsSelf();

                builder.RegisterType<ApplicationRoleManager>().AsSelf();

                builder.RegisterType<RoomTypeRepository>()
                       .As<IRoomTypeRepository>()
                       .InstancePerLifetimeScope();

                builder.RegisterType<RoomRepository>()
                       .As<IRoomRepository>()
                       .InstancePerLifetimeScope();

                builder.RegisterType<AccountService>()
                    .As<IAccountService>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<TokenService>()
                    .As<ITokenService>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<RoomService>()
                    .As<IRoomService>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<RoomTypeService>()
                    .As<IRoomTypeService>()
                    .InstancePerLifetimeScope();

                base.Load(builder);
            }
        }
    }
}