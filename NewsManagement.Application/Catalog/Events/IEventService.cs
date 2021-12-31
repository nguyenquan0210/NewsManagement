using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Catalog.Events;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Events
{
    public interface IEventService
    {
        Task<int> Create(EventCreateRequest request);

        Task<int> Update(EventUpdateRequest request);
        Task<int> Delete(int Id);

        Task<PagedResult<EventVm>> GetAllPaging(GetCatalogPagingRequest request);

        Task<List<EventVm>> GetAll();

        Task<EventVm> GetById(int Id);
    }
}
