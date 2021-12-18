using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Topic
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public Status Hot { get; set; }
    }
}
