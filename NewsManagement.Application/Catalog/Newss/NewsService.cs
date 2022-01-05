using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Application.Common;
using NewsManagement.Data.EF;
using NewsManagement.Data.Entities;
using NewsManagement.Data.Enums;
using NewsManagement.Utilities.Exceptions;
using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Newss
{
    public class NewsService : INewsService
    {
        private readonly DBContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;
        public NewsService(DBContext context, IStorageService storageService,UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }
        public async Task<int> Create(NewsCreateRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var news = new News()
            {
                Title = request.Title,
                Description = request.Description,
                Content = WebUtility.HtmlDecode(request.Content),
                Url = request.Url,
                Img = await this.SaveFile(request.ThumbnailImage),
                Video = request.Video,
                News_Hot = request.News_Hot ? Status.Active : Status.InActive,
                Keyword = request.Keyword,
                Viewss = 0,
                Status = Status.Active,
                UserId = user.Id,
                CityId = request.CityId,
                EventId = request.EventId,
                TopicId = request.TopicId,
                Date = DateTime.Now
            };
            _context.Newss.Add(news);
             await _context.SaveChangesAsync();
            return news.Id;
        }

        public async Task<int> Delete(int NewsId)
        {
            var check = 0;
            var news = await _context.Newss.FindAsync(NewsId);
            if (news == null) return check;
            if (news.Status == Status.Active)
            {
                news.Status = Status.InActive;
                check = 1;
            }
            else
            {
                _context.Newss.Remove(news);
                check = 2;
            }
            await _context.SaveChangesAsync();
            return check;
        }

      
        
        public async Task<PagedResult<NewsVm>> GetAllPaging(GetManageNewsPagingRequest request)
        {
            var query = from n in _context.Newss
                        join e in _context.Eventsses on n.EventId equals e.Id
                        join c in _context.Categories on e.CategoryId equals c.Id
                        select new { n, c };

            var user = await _userManager.FindByNameAsync(request.UserName);

            var roles = await _userManager.GetRolesAsync(user);

            var checkrole = roles.Where(x => x.ToUpper() == "ADMIN");

            if (checkrole.Count() == 0) query = query.Where(x => x.n.UserId.ToString() == user.Id.ToString());
                        
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.n.Title.Contains(request.Keyword));

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new NewsVm()
                {
                    Id = x.n.Id,
                    Title = x.n.Title,
                    Description = x.n.Description,
                    Img = x.n.Img,
                    Keyword = x.n.Keyword,
                    CateName = x.c.Name,
                    Date = x.n.Date,
                    Status = x.n.Status == Status.Active ? true : false
                }).ToListAsync();
            var pageResult = new PagedResult<NewsVm>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;

        }

        public async Task<int> Update(NewsUpdateRequest request)
        {
            var news = await _context.Newss.FindAsync(request.Id);
            if (news == null) throw new NewsManageException($"Cannot find a news with id: { request.Id}");
            news.Title = request.Title;
            news.Description = request.Description;
            news.Content = WebUtility.HtmlDecode(request.Content); 
            news.Url = request.Url;
            if(request.ThumbnailImage != null)
            {
                await _storageService.DeleteFileNewsAsync(news.Img);
                news.Img = await this.SaveFile(request.ThumbnailImage);
            }
            news.Video = request.Video;
            news.News_Hot = request.News_Hot ? Status.Active:Status.InActive;
            news.Status = request.Status ? Status.Active : Status.InActive;
            news.Keyword = request.Keyword;
            news.EventId = request.EventId;
            news.CityId = request.CityId;
            news.TopicId = request.TopicId;

            return await _context.SaveChangesAsync();
        }

        public async Task UpdateView(int NewsId)
        {
            var news = await _context.Newss.FindAsync(NewsId);
            news.Viewss += 1;
            await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileNewsAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<NewsVm> GetById(int newsId)
        {
            var news = await _context.Newss.FindAsync(newsId);

            var rs = new NewsVm()
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                Img = news.Img,
                Keyword = news.Keyword,
                Video = news.Video,
                CityId = news.CityId,
                Content = news.Content,
                EventId = news.EventId,
                TopicId = news.TopicId,
                News_Hot = news.News_Hot == Status.Active?true:false,
                Status = news.Status == Status.Active ? true : false
            };

            return rs;
        }

        public async Task<PagedResult<NewsVm>> GetAllByCategoryId(GetPublicNewsPagingRequest request)
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
                .Select(x => new NewsVm()
                {
                    Id = x.n.Id,
                    Title = x.n.Title,
                    Description = x.n.Description,
                    Img = x.n.Img,
                    Keyword =x.n.Keyword,
                    CateName = x.c.Name,
                    Status = x.c.Status == Status.Active ? true : false
                }).ToListAsync();

            var pageResult = new PagedResult<NewsVm>()
            {
                TotalRecords = totalRow,
                Items = data
            };

            return pageResult;
        }

        public async Task<List<NewsVm>> GetAll()
        {
            var query = from n in _context.Newss
                        join e in _context.Eventsses on n.EventId equals e.Id
                        join c in _context.Categories on e.CategoryId equals c.Id
                        select new { n, c };

            var data = await query.Select(x => new NewsVm()
            {
                Id = x.n.Id,
                Title = x.n.Title,
                Description = x.n.Description,
                Img = x.n.Img,
                Keyword = x.n.Keyword,
                CateName = x.c.Name

            }).ToListAsync();

            return data;
        }

        public async Task<List<NewsVm>> GetNewsTop()
        {
            var query = from n in _context.Newss
                        join e in _context.Eventsses on n.EventId equals e.Id
                        join c in _context.Categories on e.CategoryId equals c.Id
                        where n.Status == Status.Active
                        select new { n, c };
            var data = await query.Select(x => new NewsVm()
            {
                Id = x.n.Id,
                Title = x.n.Title,
                Description = x.n.Description,
                Img = x.n.Img,
                Date = x.n.Date,
                View = x.n.Viewss,
                CateName = x.c.Name,
                CategoryId = x.c.Id,
                EventId = x.n.EventId

            }).ToListAsync();

            return data;
        }
    }
}
