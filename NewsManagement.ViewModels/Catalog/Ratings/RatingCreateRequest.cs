using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Ratings
{
    public class RatingCreateRequest
    {
        public Guid UserId { get; set; }

        public int NewsId { get; set; }

        public int Value { get; set; }
    }
}
