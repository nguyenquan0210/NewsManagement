using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Servicess
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Period { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public Status Status { get; set; }
    }
}
