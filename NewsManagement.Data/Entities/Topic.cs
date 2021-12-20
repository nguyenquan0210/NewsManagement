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
        public int Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public int SortOrder { get; set; }

        public bool Hot { get; set; }

        public List<News> News { get; set; }
    }
}
