using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public Account Account { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Img { get; set; }

        public List<Order> Orders { get; set; }

    }
}
