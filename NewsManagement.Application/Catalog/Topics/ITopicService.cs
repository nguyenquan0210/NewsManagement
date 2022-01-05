using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Topics
{
    public interface ITopicService
    {
        Task<int> Create(CatalogCreateRequest request);

        Task<int> Update(CatalogUpdateRequest request);

        Task<int> Delete(int Id);

        Task<PagedResult<CatalogVm>> GetAllPaging(GetCatalogPagingRequest request);

        Task<List<CatalogVm>> GetAll();

        Task<List<CatalogVm>> GetHot();


        Task<CatalogVm> GetById(int Id);
    }
}
