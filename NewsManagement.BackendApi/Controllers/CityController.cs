﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Catalog.Cities;
using NewsManagement.ViewModels.Catalog;
using System.Threading.Tasks;

namespace NewsManagement.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CatalogCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _cityService.Create(request);
            if (newsId == 0)
                return BadRequest();

            return Ok();
        }
        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _cityService.Delete(Id);
            
            return Ok(affectedResult);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] CatalogUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _cityService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }


        //http://localhost/api/categories/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetCatalogPagingRequest request)
        {
            var user = await _cityService.GetAllPaging(request);
            return Ok(user);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var category = await _cityService.GetById(Id);
            if (category == null)
                return BadRequest("Cannot find city");
            return Ok(category);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var category = await _cityService.GetAll();
            return Ok(category);
        }
    }
}
