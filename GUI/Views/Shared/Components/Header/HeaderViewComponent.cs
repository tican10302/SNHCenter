using Microsoft.AspNetCore.Mvc;

namespace FE.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public HeaderViewComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
