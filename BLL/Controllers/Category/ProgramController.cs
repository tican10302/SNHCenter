using DTO.Base;
using DTO.Category.Program.Dtos;
using DTO.Common;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Category.Program;
using REPOSITORY.Common;

namespace BLL.Controllers.Category;

public class ProgramController(IProgramRepository repository) : BaseController<ProgramController>
{
    [HttpPost]
    [Route("get-list-paging")]
    public async Task<IActionResult> GetListPagingAsync(GetListPagingRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetListPaging(request);
            return Ok(result);
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
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
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
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
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProgramDto request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            await repository.Insert(request);
            return Created();
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProgramDto request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            await repository.Update(request);
            return Ok();
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("delete-list")]
    public async Task<IActionResult> Delete(DeleteListRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            await repository.DeLeteList(request);
            return NoContent();
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}