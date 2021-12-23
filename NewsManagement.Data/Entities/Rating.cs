using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public AppUser AppUser { get; set; }

        public int NewsId { get; set; }

        public News News { get; set; }

        public string Checkrating { get; set; }

        public int Value { get; set; }
    }
}
