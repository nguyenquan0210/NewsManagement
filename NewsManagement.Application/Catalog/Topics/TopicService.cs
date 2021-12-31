using Microsoft.EntityFrameworkCore;
using NewsManagement.Data.EF;
using NewsManagement.Data.Entities;
using NewsManagement.Data.Enums;
using NewsManagement.Utilities.Exceptions;
using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Topics
{
    public class TopicService : ITopicService
    {
        private readonly DBContext _context;

        public TopicService(DBContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CatalogCreateRequest request)
        {
            var topic = new Topic()
            {
                Name = request.Name,
                SortOrder = request.SortOrder,
                Status = Status.Active,
                Hot = request.Hot
            };
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
            return topic.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var topic = await _context.Topics.FindAsync(Id);
            if (topic == null) throw new NewsManageException($"Cannot find a Topic with id: { Id}");
            if (topic.Status == Status.Active)
            {
                topic.Status = Status.InActive;
            }
            else
            {
                _context.Topics.Remove(topic);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<List<CatalogVm>> GetAll()
        {
            var query = _context.Topics.Where(x => x.Status == Status.Active);

            return await query.Select(x => new CatalogVm()
            {
                Id = x.Id,
                Name = x.Name,
                SortOrder = x.SortOrder,
                Status = x.Status,
                Hot = x.Hot
            }).ToListAsync();
        }

        public async Task<PagedResult<CatalogVm>> GetAllPaging(GetCatalogPagingRequest request)
        {
            var query = _context.Topics;
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query.Where(x => x.Name.Contains(request.Keyword));

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CatalogVm()
                {
                    Name = x.Name,
                    SortOrder = x.SortOrder,
                    Id = x.Id,
                    Status = x.Status,
                    Hot =  x.Hot

                }).ToListAsync();

            var pagedResult = new PagedResult<CatalogVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<CatalogVm> GetById(int Id)
        {
            var topic = await _context.Topics.FindAsync(Id);

            var rs = new CatalogVm()
            {
                Id = topic.Id,
                Name = topic.Name,
                SortOrder = topic.SortOrder,
                Status = topic.Status,
                Hot = topic.Hot

            };

            return rs;
        }

        public async Task<int> Update(CatalogUpdateRequest request)
        {
            var topic = await _context.Topics.FindAsync(request.Id);
            if (topic == null) throw new NewsManageException($"Cannot find a Topic with id: { request.Id}");
            topic.Name = request.Name;
            topic.SortOrder = request.SortOrder;
            topic.Status = request.Status ? Status.Active : Status.InActive;
            topic.Hot = request.Hot;

            return await _context.SaveChangesAsync();
        }

    }
}
