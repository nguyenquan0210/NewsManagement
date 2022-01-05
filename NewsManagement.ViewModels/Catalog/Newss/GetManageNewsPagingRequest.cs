using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog.Newss
{
    public class GetManageNewsPagingRequest : PagingResultBase
    {
        public string Keyword { get; set; }
        public string UserName { get; set; }
    }
}
