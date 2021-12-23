using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Catalog.Newss;
using NewsManagement.ViewModels.Catalog.Newss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsManagement.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewssController : ControllerBase
    {
        private readonly IPublicNewsService _publicNewsSevice;
        private readonly IManageNewsService _manageNewsService;
        public NewssController(IPublicNewsService publicNewsSevice, IManageNewsService manageNewsService)
        {
            _publicNewsSevice = publicNewsSevice;
            _manageNewsService = manageNewsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var news = await _publicNewsSevice.GetAll();
            return Ok(news);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetPublicNewsPagingRequest request)
        {
            var news = await _publicNewsSevice.GetAllByCategoryId(request);
            return Ok(news);
        }
        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetById(int newsId)
        {
            var news = await _manageNewsService.GetById(newsId);
            if (news == null)
                return BadRequest("Cannot find product");
            return Ok(news);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NewsCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _manageNewsService.Create(request);
            if (newsId == 0)
                return BadRequest();

            var news = await _manageNewsService.GetById(newsId);

            return CreatedAtAction(nameof(GetById), new { id = newsId }, news);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] NewsUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageNewsService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("{newsId}")]
        public async Task<IActionResult> Delete([FromRoute] int newsId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageNewsService.Delete(newsId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
