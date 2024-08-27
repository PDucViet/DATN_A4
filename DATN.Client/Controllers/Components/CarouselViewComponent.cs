using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Controllers.Components
{
    public class CarouselViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
