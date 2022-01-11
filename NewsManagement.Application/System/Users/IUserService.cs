﻿using NewsManagement.ViewModels.Common;
using NewsManagement.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Application.System.Users
{
    public interface IUserService
    {
        
        Task<ApiResult<string>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> ChangePassword(ChangePasswordRequest request);

        Task<ApiResult<bool>> Register(PublicRegisterRequest request);

        Task<ApiResult<bool>> ManageRegister(ManageRegisterRequest request);

        Task<ApiResult<bool>> AddRoleUser(RequestRoleUser request);

        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

        ApiResult<PagedResult<UserVm>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUsersAllPaging(GetUserPagingRequest request);

        Task<ApiResult<UserVm>> GetById(Guid id);

        Task<ApiResult<UserVm>> GetByUserName(string username);

        Task<int> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
