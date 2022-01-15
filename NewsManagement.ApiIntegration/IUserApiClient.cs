using NewsManagement.ViewModels.Common;
using NewsManagement.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ApiIntegration
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUsersPagings(GetUserPagingRequest request);

        Task<ApiResult<bool>> RegisterUser(ManageRegisterRequest registerRequest);

        Task<ApiResult<bool>> PublicRegisterUser(PublicRegisterRequest registerRequest);

        Task<ApiResult<bool>> AddUserRole(RequestRoleUser request);

        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);

        Task<ApiResult<bool>> ChangePassword(ChangePasswordRequest request);

        Task<ApiResult<UserVm>> GetById(Guid id);

        Task<ApiResult<UserVm>> GetByUserName(string username);

        Task<int> Delete(Guid Id);

        Task<ApiResult<bool>> UpdateStatus(Guid id, UserUpdateStatusRequest request);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
