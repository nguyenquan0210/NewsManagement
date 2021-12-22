using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Catalog.Newss.Public;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Newss
{
    public interface IPublicNewsSevice
    {
        Task<PagedResult<NewsViewModel>> GetAllByCategoryId(GetNewsPagingRequest request);
    }
}
