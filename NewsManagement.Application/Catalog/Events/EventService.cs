using Microsoft.EntityFrameworkCore;
using NewsManagement.Data.EF;
using NewsManagement.Data.Entities;
using NewsManagement.Data.Enums;
using NewsManagement.Utilities.Exceptions;
using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Catalog.Events;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Events
{
    public class EventService : IEventService
    {
        private readonly DBContext _context;

        public EventService(DBContext context)
        {
            _context = context;
        }
        public async Task<int> Create(EventCreateRequest request)
        {
            var eventss = new Eventss()
            {
                Name = request.Name,
                SortOrder = request.SortOrder,
                Status = Status.Active,
                Hot = request.Hot,
                CategoryId = request.CategoryId
                
            };
            _context.Eventsses.Add(eventss);
            await _context.SaveChangesAsync();
            return eventss.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var eventss = await _context.Eventsses.FindAsync(Id);
            if (eventss == null) throw new NewsManageException($"Cannot find a Event with id: { Id}");
            if (eventss.Status == Status.Active)
            {
                eventss.Status = Status.InActive;
            }
            else
            {
                _context.Eventsses.Remove(eventss);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<List<EventVm>> GetAll()
        {
            var query = from e in _context.Eventsses
                        join c in _context.Categories on e.CategoryId equals c.Id
                        where e.Status == Status.Active && c.Status == Status.Active
                        select new { e, c};

            return await query.Select(x => new EventVm()
            {
                Id = x.e.Id,
                Name = x.e.Name,
                Status = x.e.Status == Status.Active ? true: false,
                NameCate = x.c.Name,
                CategoryId = x.e.Id
            }).ToListAsync();
        }

        public async Task<PagedResult<EventVm>> GetAllPaging(GetCatalogPagingRequest request)
        {
            var query = from e in _context.Eventsses
                        join c in _context.Categories on e.CategoryId equals c.Id
                        select new { e, c };

            if (!string.IsNullOrEmpty(request.Keyword))
              query =  query.Where(x => x.e.Name.Contains(request.Keyword));

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new EventVm()
                {
                    Id = x.e.Id,
                    Name = x.e.Name,
                    Status = x.e.Status == Status.Active ? true : false,
                    NameCate = x.c.Name,
                    Hot = x.e.Hot,
                    SortOrder = x.e.SortOrder
                }).ToListAsync();

            var pagedResult = new PagedResult<EventVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<EventVm> GetById(int Id)
        {
            var eventss = await _context.Eventsses.FindAsync(Id);
            if (eventss == null) throw new NewsManageException($"Cannot find a Event with id: { Id}");
            var cate = await _context.Categories.FindAsync(eventss.CategoryId);
            var rs = new EventVm()
            {
                Id = eventss.Id,
                Name = eventss.Name,
                Status = eventss.Status == Status.Active ? true : false,
                NameCate = cate.Name,
                Hot = eventss.Hot,
                CategoryId = cate.Id,
                SortOrder = eventss.SortOrder
            };
            return rs;
        }

        public async Task<List<EventVm>> GetEventHot()
        {
            var query =  _context.Eventsses.Where(x => x.Status == Status.Active && x.Hot);

            return await query.Select(x => new EventVm()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<int> Update(EventUpdateRequest request)
        {
            var eventss = await _context.Eventsses.FindAsync(request.Id);
            if (eventss == null) throw new NewsManageException($"Cannot find a Event with id: { request.Id}");
            eventss.Name = request.Name;
            eventss.SortOrder = request.SortOrder;
            eventss.Hot = request.Hot;
            eventss.CategoryId = request.CategoryId;
            eventss.Status = request.Status ? Status.Active : Status.InActive;

            return await _context.SaveChangesAsync();
        }
    }
}
