using NewsManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public int AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Status Status { get; set; }

        public DateTime Date { get; set; }

        public User Users { get; set; }

        public Staff Staff { get; set; }

        public Client Client { get; set; }

        public List<News> News { get; set; }
    }
}
