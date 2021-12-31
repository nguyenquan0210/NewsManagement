using Microsoft.AspNetCore.Http;
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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Newss
{
    public class NewsService : INewsService
    {
        private readonly DBContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public NewsService(DBContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> Create(NewsCreateRequest request)
        {
            var news = new News()
            {
                Title = request.Title,
                Description = request.Description,
                Content = request.Content,
                Url = request.Url,
                Img = await this.SaveFile(request.ThumbnailImage),
                Video = request.Video,
                News_Hot = request.News_Hot,
                Keyword = request.Keyword,
                Viewss = 0,
                Status = Status.Active,
                UserId = request.UserId,
                CityId = request.CityId,
                EventId = request.EventId,
                TopicId = request.TopicId
            };
            _context.Newss.Add(news);
             await _context.SaveChangesAsync();
            return news.Id;
        }

        public async Task<int> Delete(int NewsId)
        {
            var news = await _context.Newss.FindAsync(NewsId);
            if (news == null) throw new NewsManageException($"Cannot find a news with id: { NewsId}");
            if (news.Status == Status.Active)
            {
                news.Status = Status.InActive;
            }
            else
            {
                _context.Newss.Remove(news);
            }
            
            return await _context.SaveChangesAsync();
        }

      
        
        public async Task<PagedResult<NewsVm>> GetAllPaging(GetManageNewsPagingRequest request)
        {
            var query = from n in _context.Newss
                        join e in _context.Eventsses on n.EventId equals e.Id
                        join c in _context.Categories on e.CategoryId equals c.Id
                        select new {n, c}
                        ;
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.n.Title.Contains(request.Keyword));
            if (request.CategoryIds.Count > 0)
                query = query.Where(x => request.CategoryIds.Contains(x.c.Id));

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new NewsVm()
                {

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
            news.Content = request.Content;
            news.Url = request.Url;
            if(request.ThumbnailImage != null)
            {
                await _storageService.DeleteFileAsync(news.Img);
                news.Img = await this.SaveFile(request.ThumbnailImage);

            }

            news.Video = request.Video;
            news.News_Hot = request.News_Hot;
            news.Status = request.Status;
            news.Keyword = request.Keyword;
            news.UserId = request.UserId;
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
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
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
                Keyword = news.Keyword
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
                    CateName = x.c.Name
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

    }
}
