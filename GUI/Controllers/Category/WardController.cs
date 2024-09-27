using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;
using DTO.Base;
using DTO.Category.Ward.Dtos;
using DTO.Category.Ward.Models;
using DTO.Common;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DTO.Category.Ward.Models;

namespace GUI.Controllers.Category
{
    public class WardController : BaseController<WardController>
    {
        public IActionResult Index()
        {
            return View("~/Views/Category/Ward/Index.cshtml", GetPerMission());
        }
        public IActionResult GetList(GetListPagingRequest param)
        {
            try
            {
                var dataResult = new GetListPagingResponse();
                var result = new List<WardModel>();

                ResponseData response = this.PostAPI(UrlApi.WARD_GETLIST, param);
                if (response.Status)
                {
                    dataResult = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                    result = JsonConvert.DeserializeObject<List<WardModel>>(dataResult.Data.ToString());
                }
                else
                {
                    throw new Exception(response.Message);
                }

                return Json(new { total = dataResult.TotalRow, data = result });
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message);
            }
        }
        public ActionResult ShowViewPopup(Guid id)
        {
            try
            {
                WardModel obj = new WardModel();

                if (id != null)
                {
                    ResponseData response = this.PostAPI(UrlApi.WARD_GETBYID, new { Id = id });

                    if (response.Status)
                    {
                        obj = JsonConvert.DeserializeObject<WardModel>(response.Data.ToString());
                    }
                }

                return PartialView("~/Views/Category/Ward/PopupView.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/ErrorPartial.cshtml");
            }
        }
    }
}
