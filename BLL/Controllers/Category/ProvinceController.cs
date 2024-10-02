using DTO.Base;
using DTO.Common;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Category.Province;
using REPOSITORY.Common;

namespace BLL.Controllers.Category;

public class ProvinceController(IProvinceRepository repository) : BaseController<ProvinceController>
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetById(id);
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
}