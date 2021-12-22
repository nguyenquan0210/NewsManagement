using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Catalog.Newss.Manage;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Newss
{
    public interface IManageNewsSevice
    {
        Task<int> Create(NewsCreateRequest request);

        Task<int> Update(NewsUpdateRequest request);

        Task UpdateView(int NewsId);


        Task<int> Delete(int newsId);

       

        Task<PagedResult<NewsViewModel>> GetAllPaging(GetNewsPagingRequest request);
    }
}
