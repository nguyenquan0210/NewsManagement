using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Catalog.Ratings;
using NewsManagement.ViewModels.Catalog.Ratings;
using System.Threading.Tasks;

namespace NewsManagement.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpPost]
        public async Task<IActionResult> Rating([FromBody] RatingCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            var rating = await _ratingService.Create(request);
          
            return Ok(rating);
        }

        [HttpGet("all/{newsid}")]
        public async Task<IActionResult> GetAll(int newsid)
        {
            var rating = await _ratingService.GetList(newsid);
            return Ok(rating);
        }
    }
}
