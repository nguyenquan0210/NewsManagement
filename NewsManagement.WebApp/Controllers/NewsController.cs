using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using NewsManagement.Utilities.Constants;
using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Catalog.Ratings;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsApiClient _newsApiClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IEventApiClient _eventApiClient;
        private readonly ITopicApiClient _topicApiClient;
        private readonly ICityApiClient _cityApiClient;
        private readonly IUserApiClient _userApiClient;

        public NewsController(INewsApiClient newsApiClient,
            IConfiguration configuration,
            IEventApiClient eventApiClient,
            ITopicApiClient topicApiClient,
            ICityApiClient cityApiClient, ICategoryApiClient categoryApiClient,
            IUserApiClient userApiClient)
        {
            _newsApiClient = newsApiClient;
            _configuration = configuration;
            _eventApiClient = eventApiClient;
            _topicApiClient = topicApiClient;
            _cityApiClient = cityApiClient;
            _categoryApiClient = categoryApiClient;
            _userApiClient = userApiClient;
        }
        public async Task<IActionResult> Index(int Id)
        {
            ViewBag.Event = await _eventApiClient.GetEvent();
            ViewBag.NewsCategory = await _newsApiClient.GetNewsTop();
            ViewBag.Rating = await _newsApiClient.GetAllRating(Id);
            ViewBag.NewsTopView = await _newsApiClient.NewsFocus(100);
            if(User.Identity.Name != null)
            {
                var userss = _userApiClient.GetByUserName(User.Identity.Name);
                ViewBag.checksave = await _newsApiClient.GetBySave(userss.Result.ResultObj.Id.ToString() + Id);
            }
            
            var session = HttpContext.Session.GetString(SystemConstants.CheckNewsId);
            if(session != null)
            {
                if (session.Contains("," + Id.ToString() + ",") == false)
                {
                    session = session + "," + Id.ToString() + ",";
                    HttpContext.Session.SetString(SystemConstants.CheckNewsId, session);
                    await _newsApiClient.AddView(Id);
                }
            }
            else
            {
                HttpContext.Session.SetString(SystemConstants.CheckNewsId, "," + Id.ToString() + ",");
                await _newsApiClient.AddView(Id);
            }
            
            
            return View(await _newsApiClient.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(string users, int idnewss, int ratings)
        {
            var userss = _userApiClient.GetByUserName(users);
            var addcomment = new RatingCreateRequest();
            addcomment.NewsId = idnewss;
            addcomment.UserId = userss.Result.ResultObj.Id;
            addcomment.Value = ratings;

            var result = await _newsApiClient.AddRating(addcomment);
            return Json(new { response = result });
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(int newsId)
        {
            var userss = _userApiClient.GetByUserName(User.Identity.Name);
            var addrating = new AddCommentRequest();
            addrating.NewsId = newsId;
            addrating.UserId = userss.Result.ResultObj.Id;
            addrating.Title = userss.Result.ResultObj.Id.ToString() + newsId;

            var result = await _newsApiClient.AddComment(addrating);
            return Json(new { response = result });
        }
        public async Task<IActionResult> ListSearch(string search)
        {
            var data = await _newsApiClient.GetNewsSearch(search);
            return Json(new
            {
                data = data,
                status = true
            });
        }

        public async Task<IActionResult> NewsCategory(int categoryid)
        {
            var data = await _newsApiClient.GetNewsCategory(categoryid);
            ViewBag.Event = await _eventApiClient.GetEvent();
            ViewBag.News = await _newsApiClient.GetNewsTop();
            ViewBag.cate = categoryid;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        public async Task<IActionResult> NewsCategoryVideo(int categoryid, int pageIndex = 1, int pageSize = 20)
        {
            var request = new GetPublicNewsPagingRequest()
            {
                CategoryId = categoryid,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _newsApiClient.PublicGetAllPaging(request);
            ViewBag.cate = await _categoryApiClient.GetById(categoryid);
            ViewBag.ListNewsCate = await _newsApiClient.GetNewsCategory(categoryid);
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        public async Task<IActionResult> NewsSave()
        {
            var userss = _userApiClient.GetByUserName(User.Identity.Name);
            var data = await _newsApiClient.GetListSave(userss.Result.ResultObj.Id);
            ViewBag.Event = await _eventApiClient.GetEvent();
            ViewBag.News = await _newsApiClient.GetNewsTop();
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteSave(int saveId)
        {
            var result = await _newsApiClient.DeleteSave(saveId);
            return Json(new { response = result });
        }
        public async Task<IActionResult> NewsCity(int cityId, int pageIndex = 1, int pageSize = 18)
        {
            var request = new GetPublicNewsPagingRequest()
            {
                CityId = cityId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _newsApiClient.PublicGetAllPaging(request);
            ViewBag.city = await _cityApiClient.GetById(cityId);
            ViewBag.Listcity = await _cityApiClient.GetCity();
            ViewBag.News = await _newsApiClient.GetNewsTop();
            ViewBag.Event = await _eventApiClient.GetEvent();
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        public async Task<IActionResult> NewsEvent(int eventId, int pageIndex = 1, int pageSize = 18)
        {
            var request = new GetPublicNewsPagingRequest()
            {
                EventId = eventId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _newsApiClient.PublicGetAllPaging(request);
            ViewBag.events = await _eventApiClient.GetById(eventId);
            ViewBag.Event = await _eventApiClient.GetEvent();
            ViewBag.News = await _newsApiClient.GetNewsTop();
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        public async Task<IActionResult> NewsTopic(int topicId, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetPublicNewsPagingRequest()
            {
                TopicId = topicId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _newsApiClient.PublicGetAllPaging(request);
            ViewBag.topic = await _topicApiClient.GetById(topicId);
            ViewBag.Event = await _eventApiClient.GetEvent();
            ViewBag.News = await _newsApiClient.GetNewsTop();
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        public async Task<IActionResult> NewsSearch(string search, int pageIndex = 1, int pageSize = 18)
        {
            var request = new GetPublicNewsPagingRequest()
            {
                Keyword = search,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _newsApiClient.PublicGetAllPaging(request);
            ViewBag.Event = await _eventApiClient.GetEvent();
            ViewBag.News = await _newsApiClient.GetNewsTop();
            ViewBag.search = search;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
        public async Task<IActionResult> NewsLatest(int pageIndex = 1, int pageSize = 18)
        {
            var request = new GetPublicNewsPagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _newsApiClient.PublicGetAllPaging(request);
            ViewBag.Event = await _eventApiClient.GetEvent();
            ViewBag.City = await _cityApiClient.GetCity();
            ViewBag.NewsTop = await _newsApiClient.GetNewsTop();
            ViewBag.NewsFocus = await _newsApiClient.NewsFocus(1);
            ViewBag.NewsTopView = await _newsApiClient.NewsFocus(100);
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
    }
}
