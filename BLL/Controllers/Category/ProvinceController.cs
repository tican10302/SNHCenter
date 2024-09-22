using BLL.Helpers;
using DTO.Base;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Category;
using System.Net;
using DTO.Category.Province.Dtos;

namespace BLL.Controllers.Category
{
    public class ProvinceController(IProvinceRepository repository) : BaseController<ProvinceController>
    {
        [HttpPost, Route("get-list-paging")]
        public async Task<IActionResult> GetListPagingAsync(GetListPagingRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = await repository.GetListPaging(request);
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

        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetById(GetByIdRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = await repository.GetById(request);
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
}
