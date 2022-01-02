using Microsoft.AspNetCore.Mvc.Rendering;
using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ApiIntegration
{
    public interface INewsApiClient
    {
        Task<bool> Create(NewsCreateRequest request);

        Task<bool> Update(NewsUpdateRequest request);

        Task<int> Delete(int Id);

        Task<PagedResult<NewsVm>> GetAllPaging(GetManageNewsPagingRequest request);

        Task<PagedResult<NewsVm>> PublicGetAllPaging(GetPublicNewsPagingRequest request);


        Task<List<SelectListItem>> GetAll(int? Id);

        Task<NewsVm> GetById(int Id);
    }
}
