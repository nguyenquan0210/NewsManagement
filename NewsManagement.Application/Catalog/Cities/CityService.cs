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

namespace NewsManagement.Application.Catalog.Cities
{
    public class CityService : ICityService
    {
        private readonly DBContext _context;

        public CityService(DBContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CatalogCreateRequest request)
        {
            var city = new City()
            {
                Name = request.Name,
                SortOrder = request.SortOrder,
                Status = Status.Active
            };
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return city.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var city = await _context.Cities.FindAsync(Id);
            if (city == null) throw new NewsManageException($"Cannot find a City with id: { Id}");
            if (city.Status == Status.Active)
            {
                city.Status = Status.InActive;
            }
            else
            {
                _context.Cities.Remove(city);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<List<CatalogVm>> GetAll()
        {
            var query = _context.Cities.Where(x => x.Status == Status.Active);

            return await query.Select(x => new CatalogVm()
            {
                Id = x.Id,
                Name = x.Name,
                SortOrder = x.SortOrder,
                Status = x.Status
            }).ToListAsync();
        }

        public async Task<PagedResult<CatalogVm>> GetAllPaging(GetCatalogPagingRequest request)
        {
            var query = _context.Cities;
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
                    Status = x.Status

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
            var city = await _context.Cities.FindAsync(Id);

            var rs = new CatalogVm()
            {
                Id = city.Id,
                Name = city.Name,
                SortOrder = city.SortOrder,
                Status = city.Status
            };

            return rs;
        }


        public async Task<int> Update(CatalogUpdateRequest request)
        {
            var city = await _context.Cities.FindAsync(request.Id);
            if (city == null) throw new NewsManageException($"Cannot find a City with id: { request.Id}");
            city.Name = request.Name;
            city.SortOrder = request.SortOrder;
            city.Status = request.Status ? Status.Active: Status.InActive;

            return await _context.SaveChangesAsync();
        }

    }
}
