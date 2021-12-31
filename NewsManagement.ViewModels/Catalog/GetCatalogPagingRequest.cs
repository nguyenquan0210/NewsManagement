using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ViewModels.Catalog
{
    public class GetCatalogPagingRequest : PagingResultBase
    {
        public string Keyword { get; set; }
    }
}
