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
        public int? EventId { get; set; }
        public int? TopicId { get; set; }
        public int? CityId { get; set; }

        public string Keyword { get; set; }
    }
}
