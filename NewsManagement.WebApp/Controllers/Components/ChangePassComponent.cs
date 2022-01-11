using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class ChangePassComponent : ViewComponent
    {
       
        public ChangePassComponent()
        {
        }
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult((IViewComponentResult)View("Default"));
        }
    }
}
