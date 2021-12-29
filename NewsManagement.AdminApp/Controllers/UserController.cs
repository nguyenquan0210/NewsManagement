using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using NewsManagement.Data.Enums;
using NewsManagement.ViewModels.System.Users;
using System;
using System.Threading.Tasks;

namespace NewsManagement.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
       /* private readonly IRoleApiClient _roleApiClient;*/

        public UserController(IUserApiClient userApiClient,
            /*IRoleApiClient roleApiClient,*/
            IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            /*_roleApiClient = roleApiClient;*/
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ManageRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RegisterUser(request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới người dùng thành công";

                var requestgetuser = await _userApiClient.GetByUserName(request.UserName);

                var roleId = new Guid("2DD4EC71-5669-42D7-9CF9-BB17220C64C7");
                var requestRoleUser = new RequestRoleUser()
                {
                    IdUser = requestgetuser.ResultObj.Id,
                    IdRole = roleId
                };
                var rs = _userApiClient.AddUserRole(requestRoleUser);

                return RedirectToAction("Index");
            }

            

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = id,
                    Address = user.Address,
                    Status = user.Status == Status.Active ? true : false
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid idUser , Status status)
        {
           if(status == Status.Active)
            {
                var updatestatus = new UserUpdateStatusRequest()
                {
                    Id = idUser,
                    Status = Status.InActive
                };
                var resultupdate = await _userApiClient.UpdateStatus(idUser, updatestatus);
                if (resultupdate.IsSuccessed)
                {
                    return Json(new { response = 1 });
                }
            }
            else
            {
                var result = await _userApiClient.Delete(idUser);
                if (result.IsSuccessed)
                {
                    return Json(new { response = 2 });
                }
            }

            return Json(new { response = 3 });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }
    }
}
