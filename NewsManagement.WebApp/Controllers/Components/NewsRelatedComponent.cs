using Microsoft.AspNetCore.Mvc;
using NewsManagement.ApiIntegration;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class NewsRelatedComponent : ViewComponent
    {
        private readonly INewsApiClient _newsApiClient;

        public NewsRelatedComponent(INewsApiClient newsApiClient)
        {

            _newsApiClient = newsApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync(string keyword)
        {
            var items = await _newsApiClient.NewsRelated(keyword);
            return View(items);
        }
    }
}
