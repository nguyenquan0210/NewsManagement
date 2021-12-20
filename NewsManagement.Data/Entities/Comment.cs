using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int NewsId { get; set; }

        public News News { get; set; }

        public int Answer { get; set; }

        public Status Type { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }
    }
}
