using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Ratings
{
    public class RatingVm
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public int NewsId { get; set; }

        public string Checkrating { get; set; }

        public int Value { get; set; }
    }
}
