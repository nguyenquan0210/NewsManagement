using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Eventss
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Status Hot { get; set; }

        public int SortOrder { get; set; }
    }
}
