using DTO.Base;
using DTO.Common;
using DTO.System.Menu.Dtos;
using DTO.System.Menu.Models;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace GUI.Controllers.System;

public class MenuController : BaseController<MenuController>
{
    public ActionResult Index()
    {
        return View("~/Views/System/Menu/Index.cshtml", GetPerMission());
    }
    
    public IActionResult GetList(MenuGetListDto param)
    {
        try
        {
            var dataResult = new GetListPagingResponse();
            var result = new List<MenuModel>();

            ResponseData response = this.PostAPI(UrlApi.MENU_GETLIST, param);
            if (response.Status)
            {
                dataResult = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                result = JsonConvert.DeserializeObject<List<MenuModel>>(dataResult.Data.ToString());
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
            MenuModel obj = new MenuModel();

            if (id != null)
            {
                ResponseData response = this.PostAPI(UrlApi.MENU_GETBYID, new { Id = id });

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<MenuModel>(response.Data.ToString());
                }
            }

            return PartialView("~/Views/System/Menu/PopupView.cshtml", obj);
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
            MenuDto obj = new MenuDto();

            ResponseData response = this.PostAPI(UrlApi.MENU_GETBYPOST, new { Id = Guid.Empty });

            if (response.Status)
            {
                obj = JsonConvert.DeserializeObject<MenuDto>(response.Data.ToString());
            }

            return PartialView("~/Views/System/Menu/PopupDetail.cshtml", obj);
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
            MenuDto obj = new MenuDto();

            ResponseData response = this.PostAPI(UrlApi.MENU_GETBYPOST, new { Id = id });

            if (response.Status)
            {
                obj = JsonConvert.DeserializeObject<MenuDto>(response.Data.ToString());
            }

            return PartialView("~/Views/System/Menu/PopupDetail.cshtml", obj);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View("~/Views/Shared/ErrorPartial.cshtml");
        }
    }

    [HttpPost]
    public JsonResult Post(MenuDto param)
    {
        try
        {
            if (param != null && ModelState.IsValid)
            {
                ResponseData response;
                if (param.IsEdit)
                {
                    response = this.PostAPI(UrlApi.MENU_UPDATE, param);
                }
                else
                {
                    response = this.PostAPI(UrlApi.MENU_INSERT, param);
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
}