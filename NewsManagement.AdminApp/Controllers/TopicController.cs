using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using NewsManagement.Data.Enums;
using NewsManagement.ViewModels.Catalog;
using System.Threading.Tasks;

namespace NewsManagement.AdminApp.Controllers
{
    public class TopicController : BaseController
    {
        private readonly ITopicApiClient _topicApiClient;
        private readonly IConfiguration _configuration;

        public TopicController(ITopicApiClient topicApiClient,
            IConfiguration configuration)
        {
            _topicApiClient = topicApiClient;
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
            var data = await _topicApiClient.GetAllPaging(request);
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

            var result = await _topicApiClient.Create(request);

            if (result)
            {
                TempData["AlertMessage"] = "Thêm mới chủ đề thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thêm mới chủ đề bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _topicApiClient.GetById(id);
            if (result != null)
            {
                var updateRequest = new CatalogUpdateRequest()
                {
                    Id = id,
                    SortOrder = result.SortOrder,
                    Status = result.Status == Status.Active ? true : false,
                    Name = result.Name,
                    Hot = result.Hot
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

            var result = await _topicApiClient.Update(request);
            if (result)
            {
                TempData["AlertMessage"] = "Thay đổi thông tin chủ đề thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thay đổi thông tin chủ đề bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _topicApiClient.Delete(Id);
            return Json(new { response = result });
        }

    }
}
