using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Catalog.Newss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsManagement.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IPublicNewsService _publicNewsSevice;
        public NewsController(IPublicNewsService publicNewsSevice)
        {
            _publicNewsSevice = publicNewsSevice;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var news = await _publicNewsSevice.GetAll();
            return Ok(news);
        }
    }
}
