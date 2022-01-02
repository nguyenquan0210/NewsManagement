using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Catalog.Events;
using NewsManagement.ViewModels.Catalog;
using NewsManagement.ViewModels.Catalog.Events;
using System.Threading.Tasks;

namespace NewsManagement.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _eventService.Create(request);
            if (newsId == 0)
                return BadRequest();

            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _eventService.Delete(Id);
            
            return Ok(affectedResult);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EventUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _eventService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        //http://localhost/api/categories/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetCatalogPagingRequest request)
        {
            var events = await _eventService.GetAllPaging(request);
            return Ok(events);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var events = await _eventService.GetById(Id);
            if (events == null)
                return BadRequest("Cannot find product");
            return Ok(events);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAll();
            return Ok(events);
        }
    }
}
