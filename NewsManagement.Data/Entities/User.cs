using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public Account Account { get; set; }

        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Img { get; set; }

        public List<ActiveUser> ActiveUsers { get; set; }

        public List<Rating> Ratings { get; set; }

        public List<Comment> Comments { get; set; }

    }
}
