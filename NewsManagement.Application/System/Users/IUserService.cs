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

        Task<ApiResult<bool>> Register(ManageRegisterRequest request);

        Task<ApiResult<bool>> ManageRegister(ManageRegisterRequest request);

        Task<ApiResult<bool>> AddRoleUser(RequestRoleUser request);

        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

        Task<ApiResult<bool>> UpdateStatus(Guid id, UserUpdateStatusRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<UserVm>> GetById(Guid id);

        Task<ApiResult<UserVm>> GetByUserName(string username);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
