using Microsoft.AspNetCore.Mvc.Rendering;
using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ApiIntegration
{
    public interface ICityApiClient
    {
        Task<bool> Create(CatalogCreateRequest request);

        Task<bool> Update(CatalogUpdateRequest request);

        Task<int> Delete(int Id);

        Task<PagedResult<CatalogVm>> GetAllPaging(GetCatalogPagingRequest request);

        Task<List<SelectListItem>> GetAll(int? Id);

        Task<List<CatalogVm>> GetCity();

        Task<CatalogVm> GetById(int Id);
    }
}
