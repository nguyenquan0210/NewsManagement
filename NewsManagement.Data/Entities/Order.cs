using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Order
    {
        public int ID { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int ServiceId { get; set; }

        public Servicess Servicess { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus Status { get; set; }
    }
}
