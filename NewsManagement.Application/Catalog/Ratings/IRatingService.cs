using NewsManagement.ViewModels.Catalog.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Ratings
{
    public interface IRatingService
    {
        Task<int> Create(RatingCreateRequest request);

        Task<int> Update(RatingUpdateRequest request);

        Task<List<RatingVm>> GetList(int NewsId);

        Task<List<NewsRating>> GetAllRating();

        Task<RatingVm> GetByCheckRating(string checkRating);
    }
}
