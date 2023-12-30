using HMS.Infrastructure.Repositories;

namespace HMS.Infrastructure.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IRoomTypeRepository RoomTypes { get; }
        IRoomRepository Rooms { get; }
    }
}