using HMS.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
