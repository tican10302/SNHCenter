using AspNetCoreHero.ToastNotification.Abstractions;
using DTO.Base;
using DTO.Category.Program.Dtos;
using DTO.Category.Program.Models;
using DTO.Common;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GUI.Controllers.Category;

public class ProgramController : BaseController<ProgramController>
{
    public ProgramController()
    {
    }
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Category/Program/Index.cshtml", GetPerMission());
    }
    
    public IActionResult GetList(GetListPagingRequest param)
    {
        try
        {
            var dataResult = new GetListPagingResponse();
            var result = new List<ProgramModel>();

            ResponseData response = this.PostAPI(URL_API.PROGRAM_GETLIST, param);
            if (response.Status)
            {
                dataResult = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                result = JsonConvert.DeserializeObject<List<ProgramModel>>(dataResult.Data.ToString());
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
            ProgramModel obj = new ProgramModel();

            if (id != null)
            {
                ResponseData response = this.PostAPI(URL_API.PROGRAM_GETBYID, new { Id = id });

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<ProgramModel>(response.Data.ToString());
                }
            }

            return PartialView("~/Views/Category/Program/PopupView.cshtml", obj);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return PartialView("~/Views/Shared/ErrorPartial.cshtml");
        }
    }

    
    public ActionResult ShowInsertPopup()
    {
        try
        {
            ProgramDto obj = new ProgramDto();

            ResponseData response = this.PostAPI(URL_API.PROGRAM_GETBYPOST, new { Id = Guid.Empty });

            if (response.Status)
            {
                obj = JsonConvert.DeserializeObject<ProgramDto>(response.Data.ToString());
            }

            return PartialView("~/Views/Category/Program/PopupDetail.cshtml", obj);
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
            ProgramDto obj = new ProgramDto();

            ResponseData response = this.PostAPI(URL_API.PROGRAM_GETBYPOST, new { Id = id });

            if (response.Status)
            {
                obj = JsonConvert.DeserializeObject<ProgramDto>(response.Data.ToString());
            }

            return PartialView("~/Views/Category/Program/PopupDetail.cshtml", obj);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return PartialView("~/Views/Shared/ErrorPartial.cshtml");
        }
    }

    [HttpPost]
    public JsonResult Post(ProgramDto param)
    {
        try
        {
            if (param != null && ModelState.IsValid)
            {
                ResponseData response;
                if (param.IsEdit)
                {
                    response = this.PostAPI(URL_API.PROGRAM_UPDATE, param);
                }
                else
                {
                    response = this.PostAPI(URL_API.PROGRAM_INSERT, param);
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
            ResponseData response = this.PostAPI(URL_API.PROGRAM_DELETELIST, new { ids = listSelectedId });
            return Json(new { IsSuccess = response.Status, Message = response.Message, Data = "" });
        }
        catch (Exception ex)
        {
            string message = "Delete error: " + ex.Message;
            return Json(new { IsSuccess = false, Message = message, Data = "" });
        }
    }
}