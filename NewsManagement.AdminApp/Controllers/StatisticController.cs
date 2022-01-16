using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NewsManagement.ApiIntegration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsManagement.AdminApp.Controllers
{
    public class StatisticController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly INewsApiClient _newsApiClient;
        private readonly IConfiguration _configuration;
        public StatisticController(IUserApiClient userApiClient,
            IConfiguration configuration, INewsApiClient newsApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _newsApiClient = newsApiClient;
        }
        public async Task<IActionResult> Index()
        {
            string day = DateTime.Now.ToString("dd");
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yyyy");
            var data = await _newsApiClient.GetNewsStatitic(month,year);
            ViewBag.Statitic = JsonConvert.SerializeObject(data);
            
            List<SelectListItem> selectListDay = new List<SelectListItem>();
            for (int i = 1; i <= 31; i++)
            {
                selectListDay.Add(new SelectListItem(text: "Ng " + i, value: i.ToString()));
            }
            List<SelectListItem> selectListMonth = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                selectListMonth.Add(new SelectListItem(text: "Thg " + i, value: i.ToString()));
            }
            List<SelectListItem> selectListYear = new List<SelectListItem>();
            for (int i = int.Parse(year) - 50; i <= int.Parse(year); i++)
            {
                selectListYear.Add(new SelectListItem(text: i.ToString(), value: i.ToString()));
            }
            ViewBag.Day = selectListDay.Select(x => new SelectListItem()
            {
                Text = x.Text,
                Value = x.Value,
                Selected = day.ToString() == x.Value
            });
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
            return View(data);
        }
        public async Task<IActionResult> GetDay(string day,string month, string year)
        {
            if (int.Parse(day) < 10)
            {
                day = "0" + day;
            }
            if (int.Parse(month) < 10)
            {
                month = "0" + month;
            }
            var data = await _newsApiClient.GetNewsStatiticDay(day, month, year);

            return Json(data);
        }
        public async Task<IActionResult> GetMonth(string month, string year)
        {
            if (int.Parse(month) < 10)
            {
                month = "0" + month;
            }
            var data = await _newsApiClient.GetNewsStatitic(month, year);

            return Json(data);
        }
        public async Task<IActionResult> GetYear(string year)
        {
            var data = await _newsApiClient.GetNewsStatiticYear(year);

            return Json(data);
        }
    }
}
