using Microsoft.AspNetCore.Mvc;
using NewsManagement.ApiIntegration;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class TopicComponent : ViewComponent
    {
        private readonly ITopicApiClient _topicApiClient;

        public TopicComponent(ITopicApiClient topicApiClient)
        {

            _topicApiClient = topicApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _topicApiClient.GetHot();
            return View(items);
        }
    }
}
