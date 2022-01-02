using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NewsManagement.ViewModels.Catalog.Newss;
using NewsManagement.ViewModels.Common;
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

        public async Task<NewsVm> GetById(int Id)
        {
            var data = await GetAsync<NewsVm>($"/api/news/{Id}");

            return data;
        }

        public async Task<PagedResult<NewsVm>> PublicGetAllPaging(GetPublicNewsPagingRequest request)
        {
            var data = await GetAsync<PagedResult<NewsVm>>(
              $"/api/category/paging?pageIndex={request.PageIndex}" +
              $"&pageSize={request.PageSize}" );

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
