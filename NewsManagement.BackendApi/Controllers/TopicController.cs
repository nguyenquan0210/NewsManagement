using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Catalog.Topics;
using NewsManagement.ViewModels.Catalog;
using System.Threading.Tasks;

namespace NewsManagement.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CatalogCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _topicService.Create(request);
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
            var affectedResult = await _topicService.Delete(Id);
           
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
            var affectedResult = await _topicService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }


        //http://localhost/api/categories/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetCatalogPagingRequest request)
        {
            var user = await _topicService.GetAllPaging(request);
            return Ok(user);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var category = await _topicService.GetById(Id);
            if (category == null)
                return BadRequest("Cannot find city");
            return Ok(category);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var category = await _topicService.GetAll();
            return Ok(category);
        }
        [HttpGet("hot")]
        public async Task<IActionResult> GetHot()
        {
            var category = await _topicService.GetHot();
            return Ok(category);
        }
    }
}
