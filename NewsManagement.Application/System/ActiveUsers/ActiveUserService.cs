using Microsoft.EntityFrameworkCore;
using NewsManagement.Data.EF;
using NewsManagement.Data.Entities;
using NewsManagement.ViewModels.System.ActiveUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.System.ActiveUsers
{
    public class ActiveUserService : IActiveUserService
    {
        private readonly DBContext _context;

        public ActiveUserService(DBContext context)
        {
            _context = context;
        }
        public async Task<int> AddActiveUser(Guid userId)
        {
            var activeUser = new ActiveUser()
            {
                UserId = userId,
                DateActive = DateTime.Now,
            };
            _context.ActiveUsers.Add(activeUser);
            await _context.SaveChangesAsync();
            return activeUser.Id;
        }

        public async Task<int> UpdateActiveUser(int Id)
        {
            var activeUser = await _context.ActiveUsers.FindAsync(Id);
            activeUser.DateActive = DateTime.Now;
            await _context.SaveChangesAsync();
            return activeUser.Id;
        }
        public async Task<List<ActiveUserVm>> ListActiveUser()
        {
            var activeuser = _context.ActiveUsers;
            return await activeuser.Select(x => new ActiveUserVm
            {
                Id = x.Id,
                UserId = x.UserId,
                DateActive = x.DateActive
            }).ToListAsync();
        }

       
    }
}
