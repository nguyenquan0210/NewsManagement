using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Newss
{
    public class AddCommentRequest
    {
        public Guid UserId { get; set; }

        public int NewsId { get; set; }

        public string Title { get; set; }

    }
}
