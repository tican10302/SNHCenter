using DTO.Base;
using DTO.Common;
using DTO.System.GroupPermission.Dtos;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.System.GroupPermission;

namespace BLL.Controllers.System;

public class GroupPermissionController(IGroupPermissionRepository repository)
    : BaseController<GroupPermissionController>
{
    [HttpPost]
    [Route("get-list")]
    public async Task<IActionResult> GetListAsync(GetListPagingRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetListPaging(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("get-all")]
    public IActionResult GetAllAsync()
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = repository.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetById(new GetByIdRequest { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-by-post/{id:guid}")]
    public async Task<IActionResult> GetByPost(Guid id)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetByPost(new GetByIdRequest { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(GroupPermissionDto request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            await repository.Insert(request);
            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(GroupPermissionDto request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            await repository.Update(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("get-all-for-combobox")]
    public IActionResult GetAllForCombobox(GetAllRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = repository.GetAllForCombobox();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}