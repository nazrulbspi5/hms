using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Infrastructure.Entities
{
    public class Room : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? RoomNo { get; set; }
        public Guid RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        public double Rate { get; set; }
    }
}
