using AspNetCoreHero.ToastNotification.Abstractions;
using DTO.Base;
using DTO.Category.Province.Dtos;
using DTO.Category.Province.Models;
using DTO.Common;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GUI.Controllers.Category
{
    public class ProvinceController : BaseController<ProvinceController>
    {
        public IActionResult Index()
        {
            return View("~/Views/Category/Province/Index.cshtml", GetPerMission());
        }
        public IActionResult GetList(GetListPagingRequest param)
        {
            try
            {
                var dataResult = new GetListPagingResponse();
                var result = new List<ProvinceModel>();

                ResponseData response = this.PostAPI(UrlApi.PROVINCE_GETLIST, param);
                if (response.Status)
                {
                    dataResult = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                    result = JsonConvert.DeserializeObject<List<ProvinceModel>>(dataResult.Data.ToString());
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
                ProvinceModel obj = new ProvinceModel();

                if (id != null)
                {
                    ResponseData response = this.PostAPI(UrlApi.PROVINCE_GETBYID, new { Id = id });

                    if (response.Status)
                    {
                        obj = JsonConvert.DeserializeObject<ProvinceModel>(response.Data.ToString());
                    }
                }

                return PartialView("~/Views/Category/Province/PopupView.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/ErrorPartial.cshtml");
            }
        }
    }
}
