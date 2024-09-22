using AspNetCoreHero.ToastNotification.Abstractions;
using DTO.Base;
using DTO.Category.District.Dtos;
using DTO.Category.District.Models;
using DTO.Category.District.Models;
using DTO.Common;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace GUI.Controllers.Category
{
    public class DistrictController : BaseController<DistrictController>
    {
        public IActionResult Index()
        {
            return View("~/Views/Category/District/Index.cshtml", GetPerMission());
        }
        public IActionResult GetList(GetListPagingRequest param)
        {
            try
            {
                var dataResult = new GetListPagingResponse();
                var result = new List<DistrictModel>();

                ResponseData response = this.PostAPI(UrlApi.DISTRICT_GETLIST, param);
                if (response.Status)
                {
                    dataResult = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                    result = JsonConvert.DeserializeObject<List<DistrictModel>>(dataResult.Data.ToString());
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
                DistrictModel obj = new DistrictModel();

                if (id != null)
                {
                    ResponseData response = this.PostAPI(UrlApi.DISTRICT_GETBYID, new { Id = id });

                    if (response.Status)
                    {
                        obj = JsonConvert.DeserializeObject<DistrictModel>(response.Data.ToString());
                    }
                }

                return PartialView("~/Views/Category/District/PopupView.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/ErrorPartial.cshtml");
            }
        }
    }
}
