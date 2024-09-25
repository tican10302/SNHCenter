using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Dapper;
using DTO.Base;
using DTO.System.Menu.Dtos;
using DTO.System.Menu.Models;
using REPOSITORY.Common;

namespace REPOSITORY.System.Menu;

[RegisterClassAsTransient]
public class MenuRepository(IUnitOfWork unitOfWork, IMapper mapper) : IMenuRepository
{
    public async Task<GetListPagingResponse> GetListPaging(MenuGetListDto request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iGroupPermission", request.GroupPermissionId, DbType.Guid);
        parameters.Add("@iPageIndex", request.Offset / request.Limit, DbType.Int32);
        parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
        parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);
        var result = await unitOfWork.GetRepository<MenuModel>()
            .ExecWithStoreProcedure("sp_Category_Menu_GetListPaging", parameters);

        var totalRow = parameters.Get<long>("@oTotalRow");
        var responseData = new GetListPagingResponse
        {
            PageIndex = request.Offset,
            Data = result,
            TotalRow = Convert.ToInt32(totalRow)
        };

        return responseData;
    }
    
    public List<MenuModel> GetAll()
    {
        var result = unitOfWork.GetRepository<DAL.Entities.Menu>()
            .GetAll(x => x.IsActived)
            .OrderBy(x => x.Sort)
            .ToList();

        var response = mapper.Map<List<MenuModel>>(result);

        return response;
    }

    public async Task<MenuModel> GetById(GetByIdRequest request)
    {
        var data = await unitOfWork.GetRepository<DAL.Entities.Menu>().GetByIdAsync(request.Id);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<MenuModel>(data);
        return result;
    }

    public async Task<MenuDto> GetByPost(GetByIdRequest request)
    {
        var result = new MenuDto();
        var data = await unitOfWork.GetRepository<DAL.Entities.Menu>().GetByIdAsync(request.Id);
        if (data == null)
        {
            result.Id = Guid.NewGuid();
            result.IsEdit = false;
        }
        else
        {
            result = mapper.Map<MenuDto>(data);
            result.IsEdit = true;
        }

        return result;
    }

    public async Task<bool> Insert(MenuDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            request.ControllerName = request.ControllerName?.ToLower().Trim();
            request.Controller = request.Controller?.ToLower().Trim();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.Menu>().Find(x => 
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }
            
            var entity = mapper.Map<DAL.Entities.Menu>(request);
            await unitOfWork.GetRepository<DAL.Entities.Menu>().AddAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();

        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
        return true;
    }

    public async Task<bool> Update(MenuDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            request.ControllerName = request.ControllerName?.ToLower().Trim();
            request.Controller = request.Controller?.ToLower().Trim();
            
            var checkData = await unitOfWork.GetRepository<DAL.Entities.Menu>().Find(x =>
                x.Id != request.Id &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var data = await unitOfWork.GetRepository<DAL.Entities.Menu>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
            }
            var entity = mapper.Map(request, data);
            await unitOfWork.GetRepository<DAL.Entities.Menu>().UpdateAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
        return true;
    }

    
}