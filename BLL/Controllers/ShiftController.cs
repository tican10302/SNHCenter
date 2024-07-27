using System.Net;
using BLL.Helpers;
using DTO.Base;
using DTO.Category.Shift.Dtos;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Category;

namespace BLL.Controllers;

public class ShiftController(IShiftRepository repository) : BaseController
{
    [HttpPost, Route("get-list-paging")]
    public async Task<IActionResult> GetListPagingAsync(GetListPagingRequest request)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
            }
            var result = await repository.GetListPagingAsync(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var result = await repository.GetAllAsync();
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] string id)
    {
        try
        {
            var isValidId = Guid.TryParse(id, out var idRequest);

            if (!isValidId)
            {
                throw new Exception("Id is not valid");
            }
            var result = await repository.GetByIdAsync(idRequest);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(ShiftDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
            }
            var result = await repository.AddAsync(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }
    
    [HttpPatch]
    public async Task<IActionResult> Update([FromQuery] string id, ShiftDto request)
    {
        try
        {
            var isValidId = Guid.TryParse(id, out var idRequest);

            if (!isValidId)
            {
                throw new Exception("Id is not valid");
            }
            if (!ModelState.IsValid)
            {
                throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
            }

            request.Id = idRequest;
            
            var result = await repository.UpdateAsync(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> Detele(DeleteListRequest request)
    {
        try
        {
            var result = await repository.DeLeteListAsync(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}