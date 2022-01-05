using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Events
{
    public class EventUpdateStatusRequest
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
