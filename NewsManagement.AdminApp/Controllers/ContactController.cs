using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using NewsManagement.ViewModels.Catalog.Contacts;
using System.Threading.Tasks;

namespace NewsManagement.AdminApp.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactApiClient _contactApiClient;
        private readonly IConfiguration _configuration;

        public ContactController(IContactApiClient contactApiClient,
            IConfiguration configuration)
        {
            _contactApiClient = contactApiClient;
            _configuration = configuration;

        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Id = 1;
            var result = await _contactApiClient.GetById();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _contactApiClient.GetById();
            if (result != null)
            {
                var updateRequest = new UpdateContactRequest()
                {
                    Email = result.Email,
                    Position = result.Position,
                    Address = result.Address,
                    Company = result.Company,
                    Leader = result.Leader,
                    License = result.License,
                    ContactAdvertise = result.ContactAdvertise,
                    Hotline = result.Hotline
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateContactRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _contactApiClient.Update(request);
            if (result)
            {
                TempData["AlertMessage"] = "Thay đổi thông tin thành công thông tin liên hệ";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thay đổi thông tin thông tin liên hệ bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }
    }
}
