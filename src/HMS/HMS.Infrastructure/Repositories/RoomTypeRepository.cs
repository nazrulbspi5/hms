using HMS.Infrastructure.DbContexts;
using HMS.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HMS.Infrastructure.Repositories
{
    public class RoomTypeRepository : Repository<RoomType, Guid>, IRoomTypeRepository
    {
        private readonly IApplicationDbContext _context;
        public RoomTypeRepository(IApplicationDbContext context) : base((DbContext) context)
        {
            _context = context;
        }
    }
}
