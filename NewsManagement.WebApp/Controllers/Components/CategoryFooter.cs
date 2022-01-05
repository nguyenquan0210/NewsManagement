using Microsoft.AspNetCore.Mvc;
using NewsManagement.ApiIntegration;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class CategoryFooter : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public CategoryFooter(ICategoryApiClient categoryApiClient)
        {

            _categoryApiClient = categoryApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _categoryApiClient.GetMenu();
            return View(items);
        }
    }
}
