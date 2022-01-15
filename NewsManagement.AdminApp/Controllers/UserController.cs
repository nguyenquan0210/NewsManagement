using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using NewsManagement.Data.Enums;
using NewsManagement.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> Index(string keyword, string rolename, int pageIndex = 1, int pageSize = 10)
        {
             if(ViewBag.Role != null)
            {
                var role = ViewBag.Role;
            }
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem(text: "Khách Hàng", value: "client"),
                new SelectListItem(text: "Người dùng", value: "user")
            };
            if (User.IsInRole("admin"))
            {
                selectListItems.Add(new SelectListItem(text: "Tất cả", value: "all"));
                selectListItems.Add(new SelectListItem(text: "Nhân viên", value: "staff"));
            }
            else
            {
                if (rolename == null)
                {
                    rolename = "user";
                }
            }
            ViewBag.rolename = selectListItems.Select(x => new SelectListItem()
            {
                Text = x.Text,
                Value = x.Value,
                Selected = rolename == x.Value
            }); 

            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                RoleName = rolename
            };
            var data = await _userApiClient.GetUsersPagings(request);
            ViewBag.Keyword = keyword;
            if(rolename != null)
                ViewBag.Role = rolename;
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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ManageRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();
            request.NameRole = "staff";
            var result = await _userApiClient.RegisterUser(request);

            if (result.IsSuccessed)
            {
                TempData["AlertMessage"] = "Thêm mới người dùng thành công";
                TempData["AlertType"] = "alert-success";
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
                    Status = user.Status == Status.Active ? true : false,
                    img = user.Img
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["AlertMessage"] = "Thay đổi thông tin người dùng thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Detailt(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserVm()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = id,
                    Address = user.Address,
                    Img = user.Img,
                    UserName = user.UserName,
                    Status = user.Status //== Status.Active ? true : false
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid idUser )
        {
            var result = await _userApiClient.Delete(idUser);
            return Json(new { response = result });
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
