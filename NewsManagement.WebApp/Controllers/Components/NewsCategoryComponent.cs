using Microsoft.AspNetCore.Mvc;
using NewsManagement.ApiIntegration;
using NewsManagement.ViewModels.Catalog.Newss;
using System.Threading.Tasks;

namespace NewsManagement.WebApp.Controllers.Components
{
    public class NewsCategoryComponent : ViewComponent
    {
        private readonly INewsApiClient _newsApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public NewsCategoryComponent(INewsApiClient newsApiClient, ICategoryApiClient categoryApiClient)
        {

            _newsApiClient = newsApiClient;
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryid, int pageIndex = 1, int pageSize = 18)
        {
            var request = new GetPublicNewsPagingRequest()
            {
                CategoryId = categoryid,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _newsApiClient.PublicGetAllPaging(request);
            ViewBag.cate = await _categoryApiClient.GetById(categoryid);
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }
    }
}
