using DTO.Base;
using DTO.Common;
using DTO.System.Account.Models;
using DTO.System.Role.Dtos;
using DTO.System.Role.Models;
using GUI.Constants;
using GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace GUI.Controllers.System
{
    public class RoleController : BaseController<RoleController>
    {
        // GET: RoleController
        public ActionResult Index()
        {
            return View("~/Views/System/Role/Index.cshtml", GetPerMission());
        }

        public IActionResult GetList(GetListPagingRequest param)
        {
            try
            {
                var dataResult = new GetListPagingResponse();
                var result = new List<RoleModel>();

                ResponseData response = this.PostAPI(UrlApi.ROLE_GETLIST, param);
                if (response.Status)
                {
                    dataResult = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                    result = JsonConvert.DeserializeObject<List<RoleModel>>(dataResult.Data.ToString());
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
        
        public IActionResult GetListRolePermission(Guid? groupId, Guid? roleId)
        {
            try
            {
                if (groupId == null) groupId = Guid.Empty;
                var result = new List<Role_PermissionModel>();
                ResponseData response = this.PostAPI(UrlApi.ROLE_GETLISTROLEPERMISSION, new GetRole_PermissionDto()
                {
                    GroupId = groupId,
                    RoleId = roleId
                });

                if (response.Status)
                {
                    result = JsonConvert.DeserializeObject<List<Role_PermissionModel>>(response.Data.ToString());
                }
                else
                {
                    throw new Exception(response.Message);
                }
                return Json(new { data = result });
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
                RoleModel obj = new RoleModel();

                if (id != null)
                {
                    ResponseData response = this.PostAPI(UrlApi.ROLE_GETBYID, new { Id = id });

                    if (response.Status)
                    {
                        obj = JsonConvert.DeserializeObject<RoleModel>(response.Data.ToString());
                    }
                }

                return PartialView("~/Views/System/Role/PopupView.cshtml", obj);
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
                RoleDto obj = new RoleDto();

                ResponseData response = this.PostAPI(UrlApi.ROLE_GETBYPOST, new { Id = Guid.Empty });

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<RoleDto>(response.Data.ToString());
                }

                return PartialView("~/Views/System/Role/PopupDetail.cshtml", obj);
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
                RoleDto obj = new RoleDto();

                ResponseData response = this.PostAPI(UrlApi.ROLE_GETBYPOST, new { Id = id });

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<RoleDto>(response.Data.ToString());
                }

                return PartialView("~/Views/System/Role/PopupDetail.cshtml", obj);
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
                var listGroup = new List<SelectListItem>();

                ResponseData response = this.PostAPI(UrlApi.GROUPPERMISSION_GETALL, new GetAllRequest());

                if (response.Status)
                {
                    var data = JsonConvert.DeserializeObject<List<GroupPermissionModel>>(response.Data.ToString());
            
                    listGroup = data.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                }

                ViewBag.RoleId = id;
                ViewBag.GroupPermission = listGroup;
                return PartialView("~/Views/System/Role/PopupPermission.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/ErrorPartial.cshtml");
            }
        }

        [HttpPost]
        public JsonResult Post(RoleDto param)
        {
            try
            {
                if (param != null && ModelState.IsValid)
                {
                    ResponseData response;
                    if (param.IsEdit)
                    {
                        response = this.PostAPI(UrlApi.ROLE_UPDATE, param);
                    }
                    else
                    {
                        response = this.PostAPI(UrlApi.ROLE_INSERT, param);
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
        public JsonResult PostRolePermission(Role_PermissionDto param)
        {
            try
            {
                var idReturn = new Guid();
                if (param != null && ModelState.IsValid)
                {
                    ResponseData response = this.PostAPI(UrlApi.ROLE_POSTROLEPERMISSION, param);

                    if (response.Status)
                    {
                        var data = JsonConvert.DeserializeObject<Role_PermissionModel>(response.Data.ToString());
                        idReturn = data.Id;
                    }
                    else
                    {
                        throw new Exception(response.Message);
                    }
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = CommonFunc.GetModelState(this.ModelState), Data = "" });
                }
                return Json(new { IsSuccess = true, Message = "", Data = idReturn });
            }
            catch (Exception ex)
            {
                string message = "Permission error: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }

        [HttpPost]
        public JsonResult Delete(List<Guid> listSelectedId)
        {
            try
            {
                ResponseData response = this.PostAPI(UrlApi.ROLE_DELETELIST, new { ids = listSelectedId });
                return Json(new { IsSuccess = response.Status, Message = response.Message, Data = "" });
            }
            catch (Exception ex)
            {
                string message = "Delete error: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }
        
        public ActionResult GetList_Combobox()
        {
            ResponseData response = this.PostAPI(UrlApi.ROLE_GETALLFORCOMBOBOX, new GetAllRequest());
            var result = JsonConvert.DeserializeObject<List<ComboboxModel>>(response.Data.ToString());
            return Json(result);
        }
    }
}
