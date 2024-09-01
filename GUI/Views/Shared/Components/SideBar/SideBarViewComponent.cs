using DTO.System.Account.Models;
using DTO.System.Menu.Models;
using GUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FE.Views.Shared.Components.SideBar
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ICacheService _cacheService;
        private readonly IHttpContextAccessor _contextAccessor;
        public SideBarViewComponent(IHttpContextAccessor contextAccessor)
        {
            _cacheService = new InMemoryCache();
            _contextAccessor = contextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            List<MenuModel> menu = new List<MenuModel>();
            List<GroupPermissionModel> groupPermission = new List<GroupPermissionModel>();
            
            string cacheMenu = _cacheService.Get<string>(User.Identity.Name + "_menu");
            if (!string.IsNullOrEmpty(cacheMenu))
            {
                menu = JsonConvert.DeserializeObject<List<MenuModel>>(cacheMenu);
            }
            string cacheGroupRole = _cacheService.Get<string>(User.Identity.Name + "_grouprole");
            if (!string.IsNullOrEmpty(cacheGroupRole))
            {
                groupPermission = JsonConvert.DeserializeObject<List<GroupPermissionModel>>(cacheGroupRole);
            }

            ViewBag.Menu = groupPermission.OrderBy(x => x.Sort).ToList();
            ViewBag.MenuItem = menu.Where(x => x.IsShowMenu).ToList();
            
            var routeData = _contextAccessor.HttpContext?.GetRouteData();
            ViewBag.CurrentController = routeData?.Values["controller"]?.ToString()?.ToLower() ?? string.Empty;
            return View();
        }
    }
}
