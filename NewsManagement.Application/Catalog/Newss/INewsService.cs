using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Newss
{
    public interface INewsService
    {
        Task<int> Create(NewsCreateRequest request);

        Task<int> Update(NewsUpdateRequest request);

        Task UpdateView(int NewsId);

        Task<int> Delete(int newsId);

        Task<PagedResult<NewsVm>> GetAllPaging(GetManageNewsPagingRequest request);

        Task<NewsVm> GetById(int newsId);

        Task<PagedResult<NewsVm>> GetAllByCategoryId(GetPublicNewsPagingRequest request);

        Task<List<NewsVm>> GetAll();

        Task<List<NewsVm>> GetNewsTop();
    }
}
