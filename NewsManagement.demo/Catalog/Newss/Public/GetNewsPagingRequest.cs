using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Newss.Public
{
    public class GetNewsPagingRequest : PagingResultBase
    {
        public int? CategoryId { get; set; }
    }
}
