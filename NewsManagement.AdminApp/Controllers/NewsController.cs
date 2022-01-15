using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using NewsManagement.ViewModels.Catalog.Newss;
using System.Net;
using System.Threading.Tasks;

namespace NewsManagement.AdminApp.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsApiClient _newsApiClient;
        private readonly IConfiguration _configuration;

        private readonly IEventApiClient _eventApiClient;
        private readonly ITopicApiClient _topicApiClient;
        private readonly ICityApiClient _cityApiClient;
       

        public NewsController(INewsApiClient newsApiClient,
            IConfiguration configuration, 
            IEventApiClient eventApiClient,
            ITopicApiClient topicApiClient,
            ICityApiClient cityApiClient
            )
        {
            _newsApiClient = newsApiClient;
            _configuration = configuration;
            _eventApiClient = eventApiClient;
            _topicApiClient = topicApiClient;
            _cityApiClient = cityApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var username = User.Identity.Name;
            var request = new GetManageNewsPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                UserName = username
            };
            var data = await _newsApiClient.GetAllPaging(request);
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
            ViewBag.Topics = await _topicApiClient.GetAll(0);
            ViewBag.Events = await _eventApiClient.GetAll(0);
            ViewBag.Cities = await _cityApiClient.GetAll(0);
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]NewsCreateRequest request)
        {
            ViewBag.Topics = await _topicApiClient.GetAll(request.TopicId );
            ViewBag.Events = await _eventApiClient.GetAll(request.EventId );
            ViewBag.Cities = await _cityApiClient.GetAll(request.CityId );
            if (!ModelState.IsValid)
                return View();

            var result = await _newsApiClient.Create(request);

            if (result)
            {
                TempData["AlertMessage"] = "Thêm mới tin tức thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thêm mới tin tức bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _newsApiClient.GetById(id);
            if (result != null)
            {
                
                ViewBag.Topics = await _topicApiClient.GetAll(result.TopicId);
                ViewBag.Events = await _eventApiClient.GetAll(result.EventId);
                ViewBag.Cities = await _cityApiClient.GetAll(result.CityId);
                var updateRequest = new NewsUpdateRequest()
                {
                    Title = result.Title,
                    Description = result.Description,
                    Content = result.Content,
                    Img = result.Img,
                    Video = result.Video,
                    News_Hot = result.News_Hot,
                    Keyword = result.Keyword,
                    CityId = result.CityId,
                    EventId = result.EventId,
                    TopicId = result.TopicId,
                    Status = result.Status
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] NewsUpdateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Topics = await _topicApiClient.GetAll(request.TopicId);
            ViewBag.Events = await _eventApiClient.GetAll(request.EventId);
            ViewBag.Cities = await _cityApiClient.GetAll(request.CityId);

            var result = await _newsApiClient.Update(request);
            if (result)
            {
                TempData["AlertMessage"] = "Thay đổi thông tin tin tức thành công";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = "Thay đổi thông tin tin tức bị lỗi";
            TempData["AlertType"] = "alert-warning";
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _newsApiClient.Delete(Id);
            return Json(new { response = result });
        }
    }
}
