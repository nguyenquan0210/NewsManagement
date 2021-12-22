using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Newss
{
    public class GetPublicNewsPagingRequest : PagingResultBase
    {
        public int? CategoryId { get; set; }
    }
}
