using Microsoft.AspNetCore.Mvc;
using NewsManagement.ViewModels.Common;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
