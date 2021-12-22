using Microsoft.EntityFrameworkCore;
using NewsManagement.Data.EF;
using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Catalog.Newss.Public;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Newss
{
    public class PublicNewsSevice : IPublicNewsSevice
    {
        private readonly DBContext _context;
        /* private readonly IStorageService _storageService;*/
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public PublicNewsSevice(DBContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<NewsViewModel>> GetAllByCategoryId(GetNewsPagingRequest request)
        {
            var query = from n in _context.Newss
                        join e in _context.Eventsses on n.EventId equals e.Id
                        join c in _context.Categories on e.CategoryId equals c.Id
                        select new { n, c };

            if (request.CategoryId.Value > 0 && request.CategoryId.HasValue)
                query = query.Where(x => x.c.Id == request.CategoryId);

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new NewsViewModel()
                {

                }).ToListAsync();

            var pageResult = new PagedResult<NewsViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };

            return pageResult;
        }
    }
}
