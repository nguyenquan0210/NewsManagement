using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsManagement.Utilities.Constants;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class LoginAdminComponent : ViewComponent
    {
       

        public LoginAdminComponent()
        {

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.token = HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            return View();
        }
    }
}
