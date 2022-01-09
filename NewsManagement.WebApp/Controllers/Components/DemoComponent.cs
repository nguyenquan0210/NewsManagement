using Microsoft.AspNetCore.Mvc;
using NewsManagement.ApiIntegration;
using NewsManagement.ViewModels.Common;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class DemoComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public DemoComponent(ICategoryApiClient categoryApiClient)
        {

            _categoryApiClient = categoryApiClient;
        }
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
