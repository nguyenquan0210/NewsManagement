using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Account
    {
        public int ID { get; set; }

        public int AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Status Status { get; set; }

        public DateTime Date { get; set; }
    }
}
