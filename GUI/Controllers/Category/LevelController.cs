using DTO.Base;
using DTO.Category.Level.Dtos;
using DTO.Category.Level.Models;
using DTO.Category.Level.Dtos;
using DTO.Category.Level.Models;
using DTO.Common;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GUI.Controllers.Category
{
    public class LevelController : BaseController<LevelController>
    {
        public IActionResult Index()
        {
            return View("~/Views/Category/Level/Index.cshtml", GetPerMission());
        }
        public IActionResult GetList(GetListPagingRequest param)
        {
            try
            {
                var dataResult = new GetListPagingResponse();
                var result = new List<LevelModel>();

                ResponseData response = this.PostAPI(UrlApi.LEVEL_GETLIST, param);
                if (response.Status)
                {
                    dataResult = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                    result = JsonConvert.DeserializeObject<List<LevelModel>>(dataResult.Data.ToString());
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
                LevelModel obj = new LevelModel();

                if (id != null)
                {
                    ResponseData response = this.PostAPI(UrlApi.LEVEL_GETBYID, new { Id = id });

                    if (response.Status)
                    {
                        obj = JsonConvert.DeserializeObject<LevelModel>(response.Data.ToString());
                    }
                }

                return PartialView("~/Views/Category/Level/PopupView.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/ErrorPartial.cshtml");
            }
        }


        public ActionResult ShowInsertPopup()
        {
            try
            {
                LevelDto obj = new LevelDto();

                ResponseData response = this.PostAPI(UrlApi.LEVEL_GETBYPOST, new { Id = Guid.Empty });

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<LevelDto>(response.Data.ToString());
                }

                return PartialView("~/Views/Category/Level/PopupDetail.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/ErrorPartial.cshtml");
            }
        }

        public ActionResult ShowUpdatePopup(Guid id)
        {
            try
            {
                LevelDto obj = new LevelDto();

                ResponseData response = this.PostAPI(UrlApi.LEVEL_GETBYPOST, new { Id = id });

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<LevelDto>(response.Data.ToString());
                }

                return PartialView("~/Views/Category/Level/PopupDetail.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/ErrorPartial.cshtml");
            }
        }

        [HttpPost]
        public JsonResult Post(LevelDto param)
        {
            try
            {
                if (param != null && ModelState.IsValid)
                {
                    ResponseData response;
                    if (param.IsEdit)
                    {
                        response = this.PostAPI(UrlApi.LEVEL_UPDATE, param);
                    }
                    else
                    {
                        response = this.PostAPI(UrlApi.LEVEL_INSERT, param);
                    }
                    if (!response.Status)
                    {
                        return Json(new { IsSuccess = false, Message = response.Message, Data = "" });
                    }
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = CommonFunc.GetModelState(this.ModelState), Data = "" });
                }
                return Json(new { IsSuccess = true, Message = "", Data = param.IsEdit });
            }
            catch (Exception ex)
            {
                string message = "Post error: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }

        [HttpPost]
        public JsonResult Delete(List<Guid> listSelectedId)
        {
            try
            {
                ResponseData response = this.PostAPI(UrlApi.LEVEL_DELETELIST, new { ids = listSelectedId });
                return Json(new { IsSuccess = response.Status, Message = response.Message, Data = "" });
            }
            catch (Exception ex)
            {
                string message = "Delete error: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }
    }
        
}


