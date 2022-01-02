using Microsoft.AspNetCore.Mvc.Rendering;
using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Catalog.Events;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ApiIntegration
{
    public interface IEventApiClient
    {
        Task<bool> Create(EventCreateRequest request);

        Task<bool> Update(EventUpdateRequest request);

        Task<int> Delete(int Id);

        Task<PagedResult<EventVm>> GetAllPaging(GetCatalogPagingRequest request);

        Task<List<SelectListItem>> GetAll(int? Id);

        Task<EventVm> GetById(int Id);
    }
}
