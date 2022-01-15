using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using NewsManagement.Data.Enums;
using NewsManagement.ViewModels.System.Users;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public ProfileController(IUserApiClient userApiClient,
            IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _userApiClient.GetByUserName(User.Identity.Name);
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
                    Id = user.Id,
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
        public async Task<IActionResult> Index([FromForm] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            request.Status = true;
            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["AlertMessage"] = "Thay đổi thông tin thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thay đổi thông tin bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
                return View();
            request.UserName = User.Identity.Name;
            var result = await _userApiClient.ChangePassword(request);
            if (result.IsSuccessed)
            {
                TempData["AlertMessage"] = "Đổi mật khẩu thành công";
                TempData["AlertType"] = "alert-success";
                return View();
            }
            TempData["AlertMessage"] = "Đổi mật khẩu bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }

    }
}
