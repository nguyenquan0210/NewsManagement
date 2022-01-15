using Microsoft.AspNetCore.Authorization;
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
    /*[Authorize]*/
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsSevice;
      
        public NewsController(INewsService newsService)
        {
            _newsSevice = newsService;
           
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var news = await _newsSevice.GetAll();
            return Ok(news);
        }

        [HttpGet("newstop")]
        public async Task<IActionResult> GetNewsTop()
        {
            var news = await _newsSevice.GetNewsTop();
            return Ok(news);
        }

        [HttpGet("newssave/{userId}")]
        public async Task<IActionResult> GetNewsSave(Guid userId)
        {
            var news = await _newsSevice.GetSaveList(userId);
            return Ok(news);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageNewsPagingRequest request)
        {
            var news = await _newsSevice.GetAllPaging(request);
            return Ok(news);
        }

        [HttpGet("pagingpublic")]
        public async Task<IActionResult> GetAllPagingPublic([FromQuery] GetPublicNewsPagingRequest request)
        {
            var news = await _newsSevice.GetAllByCategoryId(request);
            return Ok(news);
        }

        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetById(int newsId)
        {
            var news = await _newsSevice.GetById(newsId);
            if (news == null)
                return BadRequest("Cannot find product");
            return Ok(news);
        }
        [HttpGet("addview/{Id}")]
        public async Task<IActionResult> AddView(int Id)
        {
            await _newsSevice.UpdateView(Id);
            return Ok();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] NewsCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _newsSevice.Create(request);
            if (newsId == 0)
                return BadRequest();

            var news = await _newsSevice.GetById(newsId);

            return CreatedAtAction(nameof(GetById), new { id = newsId }, news);
        }
        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] NewsUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _newsSevice.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpPost("addcomment")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _newsSevice.AddComment(request);
         
            return Ok(newsId);
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _newsSevice.Delete(Id);
            
            return Ok(affectedResult);
        }
        [HttpDelete("deletesave/{saveId}")]
        public async Task<IActionResult> DeleteSave([FromRoute] int saveId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _newsSevice.DeleteComment(saveId);

            return Ok(affectedResult);
        }

        [HttpGet("save/{checkstring}")]
        public async Task<IActionResult> GetBySave(string checkstring)
        {
            var news = await _newsSevice.GetBySave(checkstring);
            if (news == null)
                return BadRequest("Cannot find product");
            return Ok(news);
        }
    }
}
