using NewsManagement.ViewModels.System.ActiveUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.System.ActiveUsers
{
    public interface IActiveUserService
    {
        Task<List<ActiveUserVm>> ListActiveUser();
        Task<int> AddActiveUser(Guid userId);

        Task<int> UpdateActiveUser(int Id);
    }
}
