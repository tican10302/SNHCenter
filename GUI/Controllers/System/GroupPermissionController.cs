using DTO.Base;
using DTO.Common;
using DTO.System.Account.Models;
using DTO.System.GroupPermission.Dtos;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace GUI.Controllers.System
{
    public class GroupPermissionController : BaseController<GroupPermissionController>
    {
        // GET: GroupPermissionController
        public ActionResult Index()
        {
            return View("~/Views/System/GroupPermission/Index.cshtml", GetPerMission());
        }
        
        public IActionResult GetList(GetListPagingRequest param)
        {
            try
            {
                var dataResult = new GetListPagingResponse();
                var result = new List<GroupPermissionModel>();

                ResponseData response = this.PostAPI(UrlApi.GROUPPERMISSION_GETLIST, param);
                if (response.Status)
                {
                    result = JsonConvert.DeserializeObject<List<GroupPermissionModel>>(response.Data.ToString());
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
                GroupPermissionModel obj = new GroupPermissionModel();

                if (id != null)
                {
                    ResponseData response = this.PostAPI(UrlApi.GROUPPERMISSION_GETBYID, new { Id = id });

                    if (response.Status)
                    {
                        obj = JsonConvert.DeserializeObject<GroupPermissionModel>(response.Data.ToString());
                    }
                }

                return PartialView("~/Views/System/GroupPermission/PopupView.cshtml", obj);
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
                GroupPermissionDto obj = new GroupPermissionDto();

                ResponseData response = this.PostAPI(UrlApi.GROUPPERMISSION_GETBYPOST, new { Id = Guid.Empty });

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<GroupPermissionDto>(response.Data.ToString());
                }

                return PartialView("~/Views/System/GroupPermission/PopupDetail.cshtml", obj);
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
                GroupPermissionDto obj = new GroupPermissionDto();

                ResponseData response = this.PostAPI(UrlApi.GROUPPERMISSION_GETBYPOST, new { Id = id });

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<GroupPermissionDto>(response.Data.ToString());
                }

                return PartialView("~/Views/System/GroupPermission/PopupDetail.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/ErrorPartial.cshtml");
            }
        }
        
        public ActionResult ShowPermissionPopup(Guid id)
        {
            try
            {
                var listGroup = new List<ComboboxModel>();

                ResponseData response = this.PostAPI(UrlApi.GROUPPERMISSION_GETALL, new GetAllRequest());

                if (response.Status)
                {
                    var data = JsonConvert.DeserializeObject<List<GroupPermissionModel>>(response.Data.ToString());
            
                    listGroup = data.Select(x => new ComboboxModel()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                }

                ViewBag.RoleId = id;
                ViewBag.GroupPermission = listGroup;
                return PartialView("~/Views/System/GroupPermission/PopupPermission.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/ErrorPartial.cshtml");
            }
        }

        [HttpPost]
        public JsonResult Post(GroupPermissionDto param)
        {
            try
            {
                if (param != null && ModelState.IsValid)
                {
                    ResponseData response;
                    if (param.IsEdit)
                    {
                        response = this.PostAPI(UrlApi.GROUPPERMISSION_UPDATE, param);
                    }
                    else
                    {
                        response = this.PostAPI(UrlApi.GROUPPERMISSION_INSERT, param);
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

        public JsonResult GetList_Combobox()
        {
            try
            {
                ResponseData response = this.PostAPI(UrlApi.GROUPPERMISSION_GETALLFORCOMBOBOX, new GetAllRequest());

                if (!response.Status)
                {
                    return Json(new { IsSuccess = false, Message = response.Message, Data = "" });
                }

                var result = JsonConvert.DeserializeObject<List<SelectListItem>>(response.Data.ToString());

                return Json(new { IsSuccess = true, Message = "", Data = result });
            }
            catch (Exception ex)
            {
                string message = "Get combobox error: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }

    }
}
