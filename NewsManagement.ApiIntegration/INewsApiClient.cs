using Microsoft.AspNetCore.Mvc.Rendering;
using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Catalog.Ratings;
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

        Task<bool> AddView(int Id);

        Task<int> Delete(int Id);

        Task<PagedResult<NewsVm>> GetAllPaging(GetManageNewsPagingRequest request);

        Task<PagedResult<NewsVm>> PublicGetAllPaging(GetPublicNewsPagingRequest request);

        Task<List<SelectListItem>> GetAll(int? Id);

        Task<List<NewsVm>> GetNewsTop();

        Task<List<NewsVm>> NewsRelated(string keyword);

        Task<List<string>> GetNewsSearch( string keyword);

        Task<List<NewsVm>> GetNewsCategory(int categoryId);

        Task<List<NewsVm>> NewsFocus(int day);

        Task<List<NewsVm>> NewsVideo();

        Task<NewsVm> GetById(int Id);

        Task<int> AddRating(RatingCreateRequest request);

        Task<List<RatingVm>> GetAllRating(int newsid);

        Task<int> AddComment(AddCommentRequest request);

        Task<List<NewsVm>> GetListSave(Guid userId);

        Task<AddCommentRequest> GetBySave(string checkstring);
    }
}
