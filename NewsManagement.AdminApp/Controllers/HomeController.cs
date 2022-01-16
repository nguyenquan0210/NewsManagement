﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewsManagement.AdminApp.Models;
using NewsManagement.ApiIntegration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewsManagement.AdminApp.Controllers
{
    
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserApiClient _userApiClient;
        private readonly INewsApiClient _newsApiClient;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IUserApiClient userApiClient,
            IConfiguration configuration, INewsApiClient newsApiClient)
        {
            _logger = logger;
            _userApiClient = userApiClient;
            _configuration = configuration;
            _newsApiClient = newsApiClient;
        }
       
        public async Task<IActionResult> Index()
        {
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yyyy");
            List<SelectListItem> selectListMonth = new List<SelectListItem>();
            for(int i = 1 ; i <= 12; i++)
            {
                selectListMonth.Add(new SelectListItem(text: "Tháng " + i, value: i.ToString()));
            }
            List<SelectListItem> selectListYear = new List<SelectListItem>();
            for (int i = int.Parse(year) - 50; i <= int.Parse(year); i++)
            {
                selectListYear.Add(new SelectListItem(text: i.ToString(), value: i.ToString()));
            }
            ViewBag.Month = selectListMonth.Select(x => new SelectListItem()
            {
                Text = x.Text,
                Value = x.Value,
                Selected = month.ToString() == x.Value
            });
            ViewBag.Year = selectListYear.Select(x => new SelectListItem()
            {
                Text = x.Text,
                Value = x.Value,
                Selected = year.ToString() == x.Value
            });
            ViewBag.date = JsonConvert.SerializeObject(await _userApiClient.GetActiveUserDay(month,year));
            var alluser = _userApiClient.GetNewUsers().Result;
            ViewBag.NewUser = alluser;
            ViewBag.CountAllUser = alluser.Count();
            ViewBag.UserToday = _userApiClient.GetActiveUser().Result;
            ViewBag.NewsToday = _newsApiClient.GetNewsTop().Result;
            return View();
        }
        public async Task<IActionResult> GetMonth(string month, string year)
        {
            if(int.Parse(month) < 10)
            {
                month = "0" + month;
            }
            var counts = await _userApiClient.GetActiveUserDay(month, year);

            return Json(counts);
        }

        public IActionResult Demo()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
