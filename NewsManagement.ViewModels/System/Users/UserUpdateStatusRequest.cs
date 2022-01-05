using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.System.Users
{
    public class UserUpdateStatusRequest
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
    }
}
