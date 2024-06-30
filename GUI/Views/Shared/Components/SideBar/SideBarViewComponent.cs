using Microsoft.AspNetCore.Mvc;

namespace FE.Views.Shared.Components.SideBar
{
    public class SideBarViewComponent : ViewComponent
    {
        public SideBarViewComponent()
        {
            
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
