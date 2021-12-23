using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class ActiveUser
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public AppUser AppUser { get; set; }

        public DateTime DateActive { get; set; }

        public TimeSpan? TimeActive { get; set; }

    }
}
