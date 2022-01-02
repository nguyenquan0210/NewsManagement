using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Catalog.Events;
using NewsManagement.ViewModels.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.ApiIntegration
{
    public class EventApiClient : BaseApiClient, IEventApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public EventApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> Create(EventCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/event/", httpContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<int> Delete(int Id)
        {
            return await Delete($"/api/event/" + Id);
        }

        public async Task<List<SelectListItem>> GetAll(int? Id)
        {
            var data = await GetListAsync<EventVm>($"/api/event/all");
            var select = data.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = Id.HasValue && Id.Value == x.Id
            });
            return select.ToList();
        }

        public async Task<PagedResult<EventVm>> GetAllPaging(GetCatalogPagingRequest request)
        {
            var data = await GetAsync<PagedResult<EventVm>>(
               $"/api/event/paging?pageIndex={request.PageIndex}" +
               $"&pageSize={request.PageSize}" +
               $"&keyword={request.Keyword}");

            return data;
        }

        public async Task<EventVm> GetById(int Id)
        {
            var data = await GetAsync<EventVm>($"/api/event/{Id}");
            return data;
        }

        public async Task<bool> Update(EventUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/event/", httpContent);
            return response.IsSuccessStatusCode;
        }
    }
}
