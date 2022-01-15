using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Application.Catalog.Contacts;
using NewsManagement.ViewModels.Catalog.Contacts;
using System.Threading.Tasks;

namespace NewsManagement.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateContactRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _contactService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetById()
        {
            var contact = await _contactService.GetById();
            if (contact == null)
                return BadRequest("Cannot find contact");
            return Ok(contact);
        }
    }
}
