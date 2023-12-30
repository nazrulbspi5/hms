using HMS.Infrastructure.DbContexts;
using HMS.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HMS.Infrastructure.Repositories
{
    public class RoomRepository : Repository<Room, Guid>, IRoomRepository
    {
        private readonly IApplicationDbContext _context;
        public RoomRepository(IApplicationDbContext context) : base((DbContext) context)
        {
            _context = context;
        }
    }
}
