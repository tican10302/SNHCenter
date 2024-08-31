using System.Net;
using BLL.Helpers;
using DTO.Base;
using DTO.System.Menu.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.System.Menu;

namespace BLL.Controllers.System
{
    public class MenuController : BaseController<MenuController>
    {
        private readonly IMenuRepository repository;
        public MenuController(IMenuRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost, Route("get-list")]
        public async Task<IActionResult> GetListAsync(GetAllRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = await repository.GetList(request);
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
        
        [HttpPost, Route("get-all")]
        public async Task<IActionResult> GetAllAsync(GetAllRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = await repository.GetAll(request);
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

        [HttpPost("get-by-post")]
        public async Task<IActionResult> GetByPost(GetByIdRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = await repository.GetByPost(request);
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

        [HttpPost("insert")]
        public async Task<IActionResult> Post(MenuDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = await repository.Insert(request);
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

        [HttpPost("update")]
        public async Task<IActionResult> Update(MenuDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = await repository.Update(request);
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
