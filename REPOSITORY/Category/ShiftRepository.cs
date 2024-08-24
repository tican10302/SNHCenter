using System.Data;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DAL.Entities;
using Dapper;
using DTO.Base;
using DTO.Category.Shift.Models;
using DTO.Category.Shift.Dtos;
using REPOSITORY.Common;
using Microsoft.AspNetCore.Http;

namespace REPOSITORY.Category;

[RegisterClassAsTransient]
public class ShiftRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IShiftRepository
{
    public async Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request)
    {
        var response = new BaseResponse<GetListPagingResponse>();

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iTextSearch", request.Search, DbType.String);
            parameters.Add("@iPageIndex", request.Offset / request.Limit, DbType.Int32);
            parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
            parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

            var result = await unitOfWork.GetRepository<ShiftModel>().ExecWithStoreProcedure("sp_Category_Shift_GetListPaging", parameters);

            foreach (var item in result)
            {
                item.SelectDays = item.Days.Split(", ").ToList();
            }

            var totalRow = parameters.Get<long>("@oTotalRow");
            var responseData = new GetListPagingResponse
            {
                PageIndex = request.Offset,
                Data = result,
                TotalRow = Convert.ToInt32(totalRow)
            };

            response.Data = responseData;
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<ShiftModel>> GetById(GetByIdRequest request)
    {
        var response = new BaseResponse<ShiftModel>();

        try
        {
            var data = await unitOfWork.GetRepository<Shift>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }

            var result = mapper.Map<ShiftModel>(data);
            result.SelectDays = result.Days.Split(", ").ToList();
            response.Data = result;
        }
        catch(Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<ShiftDto>> GetByPost(GetByIdRequest request)
    {
        var response = new BaseResponse<ShiftDto>();

        try
        {
            var result = new ShiftDto();
            var data = await unitOfWork.GetRepository<Shift>().GetByIdAsync(request.Id);
            if (result == null)
            {
                result.Id = Guid.NewGuid();
                result.IsEdit = false;
            }
            else
            {
                result = mapper.Map<ShiftDto>(data);
                result.SelectDays = data.Days.Split(", ").ToList();
                result.IsEdit = true;
            }

            response.Data = result;

        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<ShiftModel>> Insert(ShiftDto request)
    {
        var response = new BaseResponse<ShiftModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<Shift>().Find(x => 
                !x.IsDeleted &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }
            
            var entity = mapper.Map<Shift>(request);
            request.SelectDays = request.SelectDays?.Where(x => x != "false").ToList();
            entity.Days = string.Join(", ", request.SelectDays);
            entity.CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            entity.CreatedAt = DateTime.Now;;
            entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            entity.UpdatedAt = DateTime.Now;
            
            var result = await unitOfWork.GetRepository<Shift>().AddAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            
            response.Data = mapper.Map<ShiftModel>(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<ShiftModel>> Update(ShiftDto request)
    {
        var response = new BaseResponse<ShiftModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<Shift>().Find(x =>
                !x.IsDeleted &&
                x.Id != request.Id &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }

            var data = await unitOfWork.GetRepository<Shift>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }
            var entity = mapper.Map(request, data);

            entity.Days = string.Join(", ", request.SelectDays);
            request.SelectDays = request.SelectDays?.Where(x => x != "false").ToList();
            entity.Days = string.Join(", ", request.SelectDays);
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            
            await unitOfWork.GetRepository<Shift>().UpdateAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            response.Data = mapper.Map<ShiftModel>(entity);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }


    public async Task<BaseResponse<string>> DeLeteList(DeleteListRequest request)
    {
        var response = new BaseResponse<string>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            foreach (var id in request.Ids)
            {
                var entity = await unitOfWork.GetRepository<Shift>().GetByIdAsync(id);

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
                entity.DeletedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            
                await unitOfWork.GetRepository<Shift>().UpdateAsync(entity);

                await unitOfWork.SaveChangesAsync();
            }
            await unitOfWork.CommitAsync();

            response.Data = "Success";
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request)
    {
        var response = new BaseResponse<List<ComboboxModel>>();

        try
        {
            var result = unitOfWork.GetRepository<DAL.Entities.Shift>()
                .GetAll(x => !x.IsDeleted && x.IsActived)
                .OrderBy(x => x.Name)
                .ToList();

            response.Data = result.Select(x => new ComboboxModel
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(x => x.Sort).ToList();
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }
}