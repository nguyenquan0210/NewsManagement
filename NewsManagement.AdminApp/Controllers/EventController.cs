using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using NewsManagement.Data.Enums;
using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Catalog.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsManagement.AdminApp.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventApiClient _eventApiClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoryApiClient _categoryApiClient;

        public EventController(IEventApiClient eventApiClient,
            IConfiguration configuration, ICategoryApiClient categoryApiClient)
        {
            _eventApiClient = eventApiClient;
            _configuration = configuration;
            _categoryApiClient = categoryApiClient;

        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetCatalogPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _eventApiClient.GetAllPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           
            ViewBag.Categories = await _categoryApiClient.GetAll(0);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _eventApiClient.Create(request);

            if (result)
            {
                TempData["AlertMessage"] = "Thêm mới sự kiện thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thêm mới sự kiện bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _eventApiClient.GetById(id);
            if (result != null)
            {
                ViewBag.Categories = await _categoryApiClient.GetAll(result.CategoryId);
                var updateRequest = new EventUpdateRequest()
                {
                    Id = id,
                    SortOrder = result.SortOrder,
                    Status = result.Status ,
                    Name = result.Name,
                    CategoryId = result.CategoryId,
                    Hot = result.Hot
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EventUpdateRequest request)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Categories = await _categoryApiClient.GetAll(request.CategoryId);
            var result = await _eventApiClient.Update(request);
            if (result)
            {
                TempData["AlertMessage"] = "Thay đổi thông tin thể loại thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thay đổi thông tin thể loại bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _eventApiClient.Delete(Id);
            return Json(new { response = result });
        }

       
    }
}
