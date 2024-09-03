using DTO.Base;
using DTO.Common;
using DTO.Management.Teacher.Dtos;
using DTO.Management.Teacher.Models;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GUI.Controllers.Management
{
    public class TeacherController : BaseController<TeacherController>
    {
        public IActionResult Index()
    {
        return View("~/Views/Management/Teacher/Index.cshtml", GetPerMission());
    }
    
    public IActionResult GetList(GetListPagingRequest param)
    {
        try
        {
            var dataResult = new GetListPagingResponse();
            var result = new List<TeacherModel>();

            ResponseData response = this.PostAPI(UrlApi.TEACHER_GETLIST, param);
            if (response.Status)
            {
                dataResult = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                result = JsonConvert.DeserializeObject<List<TeacherModel>>(dataResult.Data.ToString());
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
            TeacherModel obj = new TeacherModel();

            if (id != null)
            {
                ResponseData response = this.PostAPI(UrlApi.TEACHER_GETBYID, new { Id = id });

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<TeacherModel>(response.Data.ToString());
                }
            }

            return PartialView("~/Views/Management/Teacher/PopupView.cshtml", obj);
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
            TeacherDto obj = new TeacherDto();

            ResponseData response = this.PostAPI(UrlApi.TEACHER_GETBYPOST, new { Id = Guid.Empty });

            if (response.Status)
            {
                obj = JsonConvert.DeserializeObject<TeacherDto>(response.Data.ToString());
            }
            
            return PartialView("~/Views/Management/Teacher/PopupDetail.cshtml", obj);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return PartialView("~/Views/Shared/ErrorPartial.cshtml");
        }
    }

    public ActionResult ShowUpdatePopup(Guid id)
    {
        try
        {
            TeacherDto obj = new TeacherDto();

            ResponseData response = this.PostAPI(UrlApi.TEACHER_GETBYPOST, new { Id = id });

            if (response.Status)
            {
                obj = JsonConvert.DeserializeObject<TeacherDto>(response.Data.ToString());
            }

            return PartialView("~/Views/Management/Teacher/PopupDetail.cshtml", obj);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View("~/Views/Shared/ErrorPartial.cshtml");
        }
    }

    [HttpPost]
    public JsonResult Post(TeacherDto param)
    {
        try
        {
            if (param != null && ModelState.IsValid)
            {
                ResponseData response;
                if (param.IsEdit)
                {
                    response = this.PostAPI(UrlApi.TEACHER_UPDATE, param);
                }
                else
                {
                    response = this.PostAPI(UrlApi.TEACHER_INSERT, param);
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
            ResponseData response = this.PostAPI(UrlApi.TEACHER_DELETELIST, new { ids = listSelectedId });
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
