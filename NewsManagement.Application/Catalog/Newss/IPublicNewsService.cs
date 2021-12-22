using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Newss
{
    public interface IPublicNewsService
    {
        Task<PagedResult<NewsViewModel>> GetAllByCategoryId(GetPublicNewsPagingRequest request);

        Task<List<NewsViewModel>> GetAll();
    }
}
