using DTO.Base;
using DTO.Category.Shift.Dtos;
using DTO.Category.Shift.Requests;
using DTO.Common;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GUI.Controllers.Category;

public class ShiftController : BaseController<ShiftController>
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Category/Shift/Index.cshtml", GetPerMission());
    }
    
    public IActionResult GetList(GetListPagingRequest param)
    {
        try
        {
            var result = new List<ShiftModel>();

            ResponseData response = this.PostAPI(URL_API.SHIFT_GETLIST, param);
            if (response.Status)
            {
                var dataResult = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                result = JsonConvert.DeserializeObject<List<ShiftModel>>(dataResult.Data.ToString());
            }
            else
            {
                throw new Exception(response.Message);
            }

            return Json(result);
        }
        catch (Exception ex)
        {
            return Json("Error");
        }
    }

public ActionResult ShowViewPopup(Guid id)
{
    try
    {
        ShiftModel obj = new ShiftModel();

        if (id != null)
        {
            ResponseData response = this.PostAPI(URL_API.SHIFT_GETBYID, new { Id = id });

            if (response.Status)
            {
                obj = JsonConvert.DeserializeObject<ShiftModel>(response.Data.ToString());
            }
        }

        return PartialView("~/Views/Category/Shift/PopupView.cshtml", obj);
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
        ShiftDto obj = new ShiftDto();

        ResponseData response = this.PostAPI(URL_API.SHIFT_GETBYPOST, new { Id = Guid.Empty });

        if (response.Status)
        {
            obj = JsonConvert.DeserializeObject<ShiftDto>(response.Data.ToString());
        }

        return PartialView("~/Views/Category/Shift/PopupDetail.cshtml", obj);
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
        ShiftDto obj = new ShiftDto();

        ResponseData response = this.PostAPI(URL_API.SHIFT_GETBYPOST, new { Id = id });

        if (response.Status)
        {
            obj = JsonConvert.DeserializeObject<ShiftDto>(response.Data.ToString());
        }

        return PartialView("~/Views/Category/Shift/PopupDetail.cshtml", obj);
    }
    catch (Exception ex)
    {
        ViewBag.ErrorMessage = ex.Message;
        return PartialView("~/Views/Shared/ErrorPartial.cshtml");
    }
}

[HttpPost]
public JsonResult Post(ShiftDto param)
{
    try
    {
        if (param != null && ModelState.IsValid)
        {
            ResponseData response;
            if (param.IsEdit)
            {
                response = this.PostAPI(URL_API.SHIFT_UPDATE, param);
            }
            else
            {
                response = this.PostAPI(URL_API.SHIFT_INSERT, param);
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
        string message = "Lỗi cập nhật thông tin: " + ex.Message;
        return Json(new { IsSuccess = false, Message = message, Data = "" });
    }
}

[HttpPost]
public JsonResult Delete(List<Guid> listSelectedId)
{
    try
    {
        ResponseData response = this.PostAPI(URL_API.SHIFT_DELETELIST, new { ids = listSelectedId }); ;
        return Json(new { IsSuccess = response.Status, Message = response.Message, Data = "" });
    }
    catch (Exception ex)
    {
        string message = "Lỗi xóa thông tin: " + ex.Message;
        return Json(new { IsSuccess = false, Message = message, Data = "" });
    }
}
}