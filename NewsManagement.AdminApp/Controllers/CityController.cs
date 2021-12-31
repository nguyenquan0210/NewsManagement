using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using NewsManagement.Data.Enums;
using NewsManagement.ViewModels.Catalog;
using System.Threading.Tasks;

namespace NewsManagement.AdminApp.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityApiClient _cityApiClient;
        private readonly IConfiguration _configuration;

        public CityController(ICityApiClient cityApiClient,
            IConfiguration configuration)
        {
            _cityApiClient = cityApiClient;
            _configuration = configuration;

        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetCatalogPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _cityApiClient.GetAllPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatalogCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _cityApiClient.Create(request);

            if (result)
            {
                TempData["AlertMessage"] = "Thêm mới thể loại thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thêm mới thể loại bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _cityApiClient.GetById(id);
            if (result != null)
            {
                var updateRequest = new CatalogUpdateRequest()
                {
                    Id = id,
                    SortOrder = result.SortOrder,
                    Status = result.Status == Status.Active ? true : false,
                    Name = result.Name
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CatalogUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _cityApiClient.Update(request);
            if (result)
            {
                TempData["AlertMessage"] = "Thay đổi thể loại thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thay đổi thể loại bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _cityApiClient.Delete(Id);
            return Json(new { response = result });
        }

    }
}
