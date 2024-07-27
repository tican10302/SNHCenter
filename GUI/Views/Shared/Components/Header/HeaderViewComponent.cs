using System.Security.Claims;
using DTO.System.Account.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FE.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        private IHttpContextAccessor _contextAccessor;
        public HeaderViewComponent(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            AccountModel account = new AccountModel();
            var HostBLL = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BLLFileUrl")
                .Value.ToString();

            foreach (var claim in _contextAccessor.HttpContext.User.Claims)
            {
                switch (claim.Type)
                {
                    case ClaimTypes.Name:
                    {
                        account.UserName = claim.Value;
                    }
                        ; break;
                    case ClaimTypes.Surname:
                    {
                        account.FirstName = claim.Value;
                    }
                        ; break;
                    case ClaimTypes.GivenName:
                    {
                        account.LastName = claim.Value;
                    }
                        ; break;
                    case ClaimTypes.Role:
                        {
                            account.Role = claim.Value;
                        }
                        ; break;
                    case ClaimTypes.Thumbprint:
                    {
                        account.Avatar = HostBLL + claim.Value;
                    }
                        ;
                        break;
                }
            }

            ViewBag.UserInfo = account;
            return View();
        }
    }
}
