using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewsManagement.ApiIntegration;
using NewsManagement.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsApiClient _newsApiClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IEventApiClient _eventApiClient;
        private readonly ITopicApiClient _topicApiClient;
        private readonly ICityApiClient _cityApiClient;

        public HomeController(ILogger<HomeController> logger,
            INewsApiClient newsApiClient,
            IConfiguration configuration,
            IEventApiClient eventApiClient,
            ITopicApiClient topicApiClient,
            ICityApiClient cityApiClient, ICategoryApiClient categoryApiClient)
        {
            _logger = logger;
            _newsApiClient = newsApiClient;
            _configuration = configuration;
            _eventApiClient = eventApiClient;
            _topicApiClient = topicApiClient;
            _cityApiClient = cityApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Event = await _eventApiClient.GetEvent();
            ViewBag.NewsHot = await _newsApiClient.GetNewsTop();
            ViewBag.NewsFocus = await _newsApiClient.NewsFocus(1);
            ViewBag.NewsTopView = await _newsApiClient.NewsFocus(100);
            ViewBag.Category = await _categoryApiClient.GetMenu();
            ViewBag.NewsCategory = await _newsApiClient.NewsFocus(100);
            ViewBag.NewsVideo = await _newsApiClient.NewsVideo();
            return View();
        }


        public IActionResult Privacy()
        {
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
