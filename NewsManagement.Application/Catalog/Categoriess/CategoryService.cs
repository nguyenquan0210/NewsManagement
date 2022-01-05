using Microsoft.EntityFrameworkCore;
using NewsManagement.Application.Catalog.Categories;
using NewsManagement.Data.EF;
using NewsManagement.Data.Entities;
using NewsManagement.Data.Enums;
using NewsManagement.Utilities.Exceptions;
using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsManagement.Application.Catalog.Categoriess
{
    public class CategoryService : ICategoryService
    {
        private readonly DBContext _context;

        public CategoryService(DBContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CatalogCreateRequest request)
        {
            var cate = new Category()
            {
                Name = request.Name,
                SortOrder = request.SortOrder,
                Status = Status.Active
            };
            _context.Categories.Add(cate);
            await _context.SaveChangesAsync();
            return cate.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var cate = await _context.Categories.FindAsync(Id);
            int check = 0;
            if (cate == null) return check;
            if(cate.Status == Status.Active)
            {
                cate.Status = Status.InActive;
                check = 1;
            }
            else
            {
                _context.Categories.Remove(cate);
                check = 2;
            }
            await _context.SaveChangesAsync();
            return check;
        }

        public async Task<List<CatalogVm>> GetAll()
        {
            var query = _context.Categories.Where(x => x.Status == Status.Active);

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
            var query = _context.Categories;
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
            var cate = await _context.Categories.FindAsync(Id);

            var rs = new CatalogVm()
            {
                Id = cate.Id,
                Name = cate.Name,
                SortOrder = cate.SortOrder,
                Status = cate.Status
            };

            return rs;
        }

        public async Task<int> Update(CatalogUpdateRequest request)
        {
            var cate = await _context.Categories.FindAsync(request.Id);
            if (cate == null) throw new NewsManageException($"Cannot find a Category with id: { request.Id}");
            cate.Name = request.Name;
            cate.SortOrder = request.SortOrder;
            cate.Status = request.Status ? Status.Active : Status.InActive;

            return await _context.SaveChangesAsync();
        }

    }
}
