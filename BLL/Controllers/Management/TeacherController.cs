using System.Net;
using BLL.Helpers;
using DTO.Base;
using DTO.Common;
using DTO.Management.Teacher.Dtos;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Management;

namespace BLL.Controllers.Management;

public class TeacherController(ITeacherRepository repository) : BaseController<TeacherController>
{
    [HttpPost]
    [Route("get-list-paging")]
    public async Task<IActionResult> GetListPagingAsync(GetListPagingRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetListPaging(request);
            if (result.Error)
                throw new Exception(result.Message);
            return Ok(new ApiOkResponse(result.Data));
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetById(GetByIdRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetById(request);
            if (result.Error)
                throw new Exception(result.Message);
            return Ok(new ApiOkResponse(result.Data));
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("get-by-post")]
    public async Task<IActionResult> GetByPost(GetByIdRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetByPost(request);
            if (result.Error)
                throw new Exception(result.Message);
            return Ok(new ApiOkResponse(result.Data));
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("insert")]
    public async Task<IActionResult> Post(TeacherDto request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.Insert(request);
            if (result.Error)
                throw new Exception(result.Message);
            return Ok(new ApiOkResponse(result.Data));
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(TeacherDto request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.Update(request);
            if (result.Error)
                throw new Exception(result.Message);
            return Ok(new ApiOkResponse(result.Data));
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("delete-list")]
    public async Task<IActionResult> Detele(DeleteListRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.DeLeteList(request);
            if (result.Error)
                throw new Exception(result.Message);
            return Ok(new ApiOkResponse(result.Data));
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}