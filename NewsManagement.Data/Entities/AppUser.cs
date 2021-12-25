using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string Address { get; set; }

        public string Img { get; set; }

        public List<ActiveUser> ActiveUsers { get; set; }

        public List<Rating> Ratings { get; set; }

        public List<Comment> Comments { get; set; }

        public List<News> News { get; set; }

        public List<Order> Orders { get; set; }

    }
}
