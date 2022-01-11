using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Catalog.Ratings;
using NewsManagement.ViewModels.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ApiIntegration
{
    public class NewsApiClient : BaseApiClient, INewsApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public NewsApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> AddComment(AddCommentRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/news/addcomment", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            return int.Parse(result);
        }

        public async Task<int> AddRating(RatingCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/rating/", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            return int.Parse(result);
        }

        public async Task<bool> AddView(int Id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/news/addview/{Id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Create(NewsCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.Title.ToString()), "title");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");

            requestContent.Add(new StringContent(request.Content.ToString()), "content");
            requestContent.Add(new StringContent(request.News_Hot.ToString()), "news_hot");
            requestContent.Add(new StringContent(request.Keyword.ToString()), "keyword");
            requestContent.Add(new StringContent(request.CityId.ToString()), "cityid");
            requestContent.Add(new StringContent(request.TopicId.ToString()), "topicid");
            requestContent.Add(new StringContent(request.UserName.ToString()), "username");
            requestContent.Add(new StringContent(request.EventId.ToString()), "eventid");

            var response = await client.PostAsync($"/api/news", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<int> Delete(int Id)
        {
            return await Delete($"/api/news/" + Id);
        }

        public async Task<List<SelectListItem>> GetAll(int? Id)
        {
            var data = await GetListAsync<NewsVm>($"/api/news/all");
            var select = data.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString(),
                Selected = Id.HasValue && Id.Value == x.Id
            });
            return select.ToList();
        }

        public async Task<PagedResult<NewsVm>> GetAllPaging(GetManageNewsPagingRequest request)
        {
            var data = await GetAsync<PagedResult<NewsVm>>(
              $"/api/news/paging?pageIndex={request.PageIndex}" +
              $"&pageSize={request.PageSize}" +
              $"&keyword={request.Keyword}" +
              $"&username={request.UserName}");
            return data;
        }

        public async Task<List<RatingVm>> GetAllRating(int newsid)
        {
            var data = await GetListAsync<RatingVm>($"/api/rating/all/{newsid}");
            
            return data;
        }

        public async Task<NewsVm> GetById(int Id)
        {
            var data = await GetAsync<NewsVm>($"/api/news/{Id}");
            return data;
        }

        public async Task<AddCommentRequest> GetBySave(string checkstring)
        {
            var data = await GetAsync<AddCommentRequest>($"/api/news/save/{checkstring}");
            return data;
        }

        public async Task<List<NewsVm>> GetListSave(Guid userId)
        {
            var data = await GetListAsync<NewsVm>($"/api/news/newssave/{userId}");
            return data;
        }

        public async Task<List<NewsVm>> GetNewsCategory(int categoryId)
        {
            var data = await GetListAsync<NewsVm>($"/api/news/newstop");
            data = data.Where(x => x.CategoryId == categoryId).ToList();
            return data.Take(5).ToList();
        }

        public async Task<List<string>> GetNewsSearch(string keyword)
        {
            var data = await GetListAsync<NewsVm>($"/api/news");
            var result =  data.Where(x => x.Title.Contains(keyword)).Take(10).Select(x=>x.Title.Substring(0, 30)).ToList();
            return result;
        }

        public async Task<List<NewsVm>> GetNewsTop()
        {
            var data = await GetListAsync<NewsVm>($"/api/news/newstop");
            return data;
        }

        public async Task<List<NewsVm>> NewsFocus(int day)
        {
            var data = await GetListAsync<NewsVm>($"/api/news/newstop");
            data = data.Where(x=>x.Date > DateTime.Now.AddMonths(-day)).ToList();

            int tg = data.Sum(x => x.View);
            int i = data.Count();
            double Viewssfocus = 0;
            if (i > 0)
            {
                Viewssfocus = tg / i;
            }
            if (day > 50)
            {
                Viewssfocus = Viewssfocus - (Viewssfocus * 20 / 100);
            }
            data = data.Where(x => x.View >= Viewssfocus).ToList();
            return data;
        }

        public async Task<List<NewsVm>> NewsRelated(string keyword)
        {
            string[] arrListStr = keyword.Split(',');
            string keyword1 = ""; string keyword2 = ""; string keyword3 = ""; string keyword4 = ""; string keyword5 = "";
            if (arrListStr.Length == 1) { keyword1 = arrListStr[0]; keyword2 = arrListStr[0]; keyword3 = arrListStr[0]; keyword4 = arrListStr[0]; keyword5 = arrListStr[0]; };
            if (arrListStr.Length == 2) { keyword1 = arrListStr[0]; keyword2 = arrListStr[1]; keyword3 = arrListStr[1]; keyword4 = arrListStr[1]; keyword5 = arrListStr[1]; };
            if (arrListStr.Length == 3) { keyword1 = arrListStr[0]; keyword2 = arrListStr[1]; keyword3 = arrListStr[2]; keyword4 = arrListStr[2]; keyword5 = arrListStr[2]; };
            if (arrListStr.Length == 4) { keyword1 = arrListStr[0]; keyword2 = arrListStr[1]; keyword3 = arrListStr[2]; keyword4 = arrListStr[3]; keyword5 = arrListStr[3]; };
            if (arrListStr.Length == 5) { keyword1 = arrListStr[0]; keyword2 = arrListStr[1]; keyword3 = arrListStr[2]; keyword4 = arrListStr[3]; keyword5 = arrListStr[4]; };
            var data = await GetListAsync<NewsVm>($"/api/news/newstop");
            data = data.Where(x => x.Title.ToUpper().Contains(keyword1.ToUpper()) || x.Title.ToUpper().Contains(keyword2.ToUpper())
            || x.Title.ToUpper().Contains(keyword3.ToUpper()) || x.Title.ToUpper().Contains(keyword4.ToUpper()) || x.Title.ToUpper().Contains(keyword5.ToUpper())).Take(8).ToList();
            return data;
        }

        public async Task<List<NewsVm>> NewsVideo()
        {
            var data = await GetListAsync<NewsVm>($"/api/news/newstop");
            data = data.Where(x => x.CateName.ToUpper() == "VIDEO" ).ToList();

            int tg = data.Sum(x => x.View);
            int i = data.Count();
            double Viewssfocus = 0;
            if (i > 0)
            {
                Viewssfocus = tg / i;
            }
            data = data.Where(x => x.View >= Viewssfocus).ToList();
            return data;
        }

        public async Task<PagedResult<NewsVm>> PublicGetAllPaging(GetPublicNewsPagingRequest request)
        {
            var data = await GetAsync<PagedResult<NewsVm>>(
              $"/api/news/pagingpublic?pageIndex={request.PageIndex}" +
              $"&pageSize={request.PageSize}" +
              $"&categoryid={request.CategoryId}" +
              $"&cityid={request.CityId}" +
              $"&topicid={request.TopicId}" +
              $"&eventid={request.EventId}" +
              $"&keyword={request.Keyword}");

            return data;
        }

        public async Task<bool> Update(NewsUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(request.Id.ToString()), "id");
            requestContent.Add(new StringContent(request.Title.ToString()), "title");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");

            requestContent.Add(new StringContent(request.Content.ToString()), "content");
            requestContent.Add(new StringContent(request.News_Hot.ToString()), "news_hot");
            requestContent.Add(new StringContent(request.Keyword.ToString()), "keyword");
            requestContent.Add(new StringContent(request.CityId.ToString()), "cityid");
            requestContent.Add(new StringContent(request.TopicId.ToString()), "topicid");
            requestContent.Add(new StringContent(request.EventId.ToString()), "eventid");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PutAsync($"/api/news", requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}
