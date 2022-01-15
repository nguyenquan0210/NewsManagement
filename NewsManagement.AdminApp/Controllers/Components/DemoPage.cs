using Microsoft.AspNetCore.Mvc;
using NewsManagement.ApiIntegration;
using NewsManagement.ViewModels.Common;
using System.Threading.Tasks;

namespace NewsManagement.AdminApp.Controllers.Components
{
    public class DemoPage : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public DemoPage(ICategoryApiClient categoryApiClient)
        {

            _categoryApiClient = categoryApiClient;
        }
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
