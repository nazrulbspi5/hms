using HMS.Infrastructure.DbContexts;
using HMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IRoomTypeRepository RoomTypes { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext, 
            IRoomTypeRepository roomTypeRepository,
            IRoomRepository roomRepository)
            : base((DbContext)dbContext)
        {
           RoomTypes = roomTypeRepository;
           Rooms = roomRepository;
        }

    }
}