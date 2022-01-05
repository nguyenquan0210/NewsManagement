using Microsoft.AspNetCore.Mvc;
using NewsManagement.ApiIntegration;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class EventComponent : ViewComponent
    {
        private readonly IEventApiClient _eventApiClient;

        public EventComponent(IEventApiClient eventApiClient)
        {

            _eventApiClient = eventApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _eventApiClient.GetEvent();
            return View(items);
        }
    }
}
