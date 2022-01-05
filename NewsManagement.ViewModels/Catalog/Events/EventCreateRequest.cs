using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Events
{
    public class EventCreateRequest
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public bool Hot { get; set; }

        public int SortOrder { get; set; }
    }
}
