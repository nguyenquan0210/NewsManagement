using NewsManagement.Application.Common;
using NewsManagement.Data.EF;
using NewsManagement.ViewModels.Common;
using NewsManagement.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly DBContext _context;
        private readonly IStorageService _storageService;
        public RoleService(DBContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
       
    }
}
