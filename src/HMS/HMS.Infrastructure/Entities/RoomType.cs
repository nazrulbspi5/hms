using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Infrastructure.Entities
{
    public class RoomType:IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? TypeName { get; set; }
    }
}
