using Microsoft.AspNetCore.Mvc;
using NewsManagement.ApiIntegration;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class FooterComponent : ViewComponent
    {
        private readonly IContactApiClient _contactApiClient;

        public FooterComponent(IContactApiClient contactApiClient)
        {
            _contactApiClient = contactApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _contactApiClient.GetById();
            return View(items);
        }
    }
}
