using System.Data;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DAL.Entities;
using Dapper;
using DTO.Base;
using DTO.Management.Teacher.Dtos;
using DTO.Management.Teacher.Models;
using Microsoft.AspNetCore.Http;
using REPOSITORY.Common;

namespace REPOSITORY.Management;


[RegisterClassAsTransient]
public class TeacherRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : ITeacherRepository
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

            var result = await unitOfWork.GetRepository<TeacherModel>().ExecWithStoreProcedure("sp_Management_Teacher_GetListPaging", parameters);

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

    public async Task<BaseResponse<TeacherModel>> GetById(GetByIdRequest request)
    {
        var response = new BaseResponse<TeacherModel>();

        try
        {
            var data = await unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }

            var result = mapper.Map<TeacherModel>(data);
            response.Data = result;
        }
        catch(Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<TeacherDto>> GetByPost(GetByIdRequest request)
    {
        var response = new BaseResponse<TeacherDto>();

        try
        {
            var result = new TeacherDto();
            var data = await unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);
            if (result == null)
            {
                result.Id = Guid.NewGuid();
                result.IsEdit = false;
            }
            else
            {
                result = mapper.Map<TeacherDto>(data);
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

    public async Task<BaseResponse<TeacherModel>> Insert(TeacherDto request)
    {
        var response = new BaseResponse<TeacherModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<User>().Find(x => 
                !x.IsDeleted &&
                x.StaffCode == request.StaffCode);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }
            
            var entity = mapper.Map<User>(request);
            entity.CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            entity.CreatedAt = DateTime.Now;;
            entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            entity.UpdatedAt = DateTime.Now;
            
            var result = await unitOfWork.GetRepository<User>().AddAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            
            response.Data = mapper.Map<TeacherModel>(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<TeacherModel>> Update(TeacherDto request)
    {
        var response = new BaseResponse<TeacherModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<User>().Find(x =>
                !x.IsDeleted &&
                x.Id != request.Id &&
                x.StaffCode == request.StaffCode);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }

            var data = await unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }
            var entity = mapper.Map(request, data);

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            
            await unitOfWork.GetRepository<User>().UpdateAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            response.Data = mapper.Map<TeacherModel>(entity);
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
                var entity = await unitOfWork.GetRepository<User>().GetByIdAsync(id);

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
                entity.DeletedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            
                await unitOfWork.GetRepository<User>().UpdateAsync(entity);

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
}