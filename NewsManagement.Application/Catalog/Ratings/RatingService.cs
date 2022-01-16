using Microsoft.EntityFrameworkCore;
using NewsManagement.Data.EF;
using NewsManagement.Data.Entities;
using NewsManagement.Utilities.Exceptions;
using NewsManagement.ViewModels.Catalog.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Ratings
{
    public class RatingService : IRatingService
    {
        private readonly DBContext _context;

        public RatingService(DBContext context)
        {
            _context = context;
        }
        public async Task<int> Create(RatingCreateRequest request)
        {
            var rs = 0;
            var check = await _context.Ratings.FirstOrDefaultAsync(x => x.Checkrating == request.UserId.ToString() + request.NewsId);
            if (check == null)
            {
                var rating = new Rating()
                {
                    UserId = request.UserId,
                    NewsId = request.NewsId,
                    Checkrating = request.UserId.ToString() + request.NewsId,
                    Value = request.Value
                };
                _context.Ratings.Add(rating);
                rs = 1;
            }
            else
            {
                var rating = await _context.Ratings.FindAsync(check.Id);
                rating.Value = request.Value;
                rs = 2;
            }
            await _context.SaveChangesAsync();
            return rs;
        }

        public async Task<List<NewsRating>> GetAllRating()
        {
            var query = from r in _context.Ratings
                        join n in _context.Newss on r.NewsId equals n.Id
                        select new  { n, r };
            var data = await query.Select(x => new NewsRating() {
                Id = x.r.Id,
                NewsId = x.r.NewsId,
                Date = x.n.Date,
                Value = x.r.Value
            }).ToListAsync();
            return data;
        }

        public async Task<RatingVm> GetByCheckRating(string checkRating)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(x=> x.Checkrating == checkRating);

            var rs = new RatingVm();
            if (rating != null)
            {
                rs.Id = rating.Id;
                rs.UserId = rating.UserId;
                rs.NewsId = rating.NewsId;
                rs.Checkrating = rating.Checkrating;
                rs.Value = rating.Value;
            }
            
            return rs;
        }

        public async Task<List<RatingVm>> GetList(int NewsId)
        {
            var query = _context.Ratings.Where(x=>x.NewsId == NewsId);

            return await query.Select(x => new RatingVm()
            {
               /* Id = x.Id,
                UserId = x.UserId,
                NewsId = x.NewsId,
                Checkrating = x.Checkrating,*/
                Value = x.Value
            }).ToListAsync();
        }

        public async Task<int> Update(RatingUpdateRequest request)
        {
            var rating = await _context.Ratings.FindAsync(request.Id);
            if (rating == null) throw new NewsManageException($"Cannot find a rating with id: { request.Id}");
            rating.Value = request.Value;

            return await _context.SaveChangesAsync();
        }
    }
}
