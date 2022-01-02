﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NewsManagement.Utilities.Constants;
using NewsManagement.ViewModels.Catalog;
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
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CategoryApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> Create(CatalogCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/category/", httpContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<int> Delete(int Id)
        {
            return await Delete($"/api/category/" + Id);
        }

        public async Task<List<SelectListItem>> GetAll(int? Id)
        {
            var data = await GetListAsync<CatalogVm>($"/api/category/all");
            var select = data.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = Id.HasValue && Id.Value == x.Id
            });
            return select.ToList();
        }

        public async Task<PagedResult<CatalogVm>> GetAllPaging(GetCatalogPagingRequest request)
        {
            var data = await GetAsync<PagedResult<CatalogVm>>(
               $"/api/category/paging?pageIndex={request.PageIndex}" +
               $"&pageSize={request.PageSize}" +
               $"&keyword={request.Keyword}");

            return data;
        }

        public async Task<CatalogVm> GetById(int Id)
        {
            var data = await GetAsync<CatalogVm>($"/api/category/{Id}");

            return data;
        }

        public async Task<bool> Update(CatalogUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/category/", httpContent);
            return response.IsSuccessStatusCode;
        }

    }
}
