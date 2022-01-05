using Microsoft.AspNetCore.Mvc;
using NewsManagement.ApiIntegration;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class CityComponent : ViewComponent
    {
        private readonly ICityApiClient _cityApiClient;

        public CityComponent(ICityApiClient cityApiClient)
        {

            _cityApiClient = cityApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _cityApiClient.GetCity();
            return View(items);
        }
    }
}
