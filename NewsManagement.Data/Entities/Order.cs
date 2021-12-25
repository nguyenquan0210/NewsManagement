using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public AppUser AppUser { get; set; }

        public int ServiceId { get; set; }

        public Servicess Servicess { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus Status { get; set; }

        public List<Advertise> Advertises { get; set; }
    }
}
