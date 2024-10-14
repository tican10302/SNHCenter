using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Dapper;
using DTO.Base;
using DTO.Management.LessonTemplate.Dtos;
using DTO.Management.LessonTemplate.Models;
using Microsoft.AspNetCore.Http;
using REPOSITORY.Common;

namespace REPOSITORY.Management.LessonTemplate;

[RegisterClassAsTransient]
public class LessonTemplateRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : ILessonTemplateRepository
{
    public async Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iPageIndex", request.Offset / request.Limit, DbType.Int32);
        parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
        parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

        var result = await unitOfWork.GetRepository<LessonTemplateModel>().ExecWithStoreProcedure("sp_Management_LessonTemplate_GetListPaging", parameters);

        var totalRow = parameters.Get<long>("@oTotalRow");
        var response = new GetListPagingResponse()
        {
            PageIndex = request.Offset,
            Data = result,
            TotalRow = Convert.ToInt32(totalRow)
        };
        return response;
    }
    
    public List<LessonTemplateModel> GetAll(GetListLessonTemplateRequest request)
    {
        var data = unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().GetAll(
            x => 
                !x.IsDeleted 
                && x.IsActive
                && (request.CourseTemplateId == Guid.Empty || request.CourseTemplateId == x.CourseTemplateId));
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<List<LessonTemplateModel>>(data);
        return result;
    }
    
    public async Task<LessonTemplateModel> GetById(GetByIdRequest request)
    {
        var data = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().GetByIdAsync(request.Id);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<LessonTemplateModel>(data);
        return result;
    }
    
    public async Task<LessonTemplateDto> GetByPost(GetByIdRequest request)
    {

        var result = new LessonTemplateDto();
        var data = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().GetByIdAsync(request.Id);

        if (data == null)
        {
            result.Id = Guid.NewGuid();
            result.IsEdit = false;
        }
        else
        {
            result = mapper.Map<LessonTemplateDto>(data);
            result.IsEdit = true;
        }

        return result;
    }
    
    public async Task<bool> InsertList(List<LessonTemplateDto> request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            foreach (var data in request)
            {
                var entity = mapper.Map<DAL.Entities.LessonTemplate>(data);
                entity.CreatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
                entity.UpdatedAt = DateTime.Now;

                await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().AddAsync(entity);
            }

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
    
    public async Task<bool> UpdateList(List<LessonTemplateDto> request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            foreach (var el in request)
            {
                var checkData = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().Find(x =>
                    !x.IsDeleted &&
                    x.Id != el.Id);
                if (checkData != null)
                {
                    throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
                }

                var data = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().GetByIdAsync(el.Id);
                if (data == null)
                {
                    throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
                }
                var entity = mapper.Map(el, data);

                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

                await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().UpdateAsync(entity);
            }

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
    
    public async Task<bool> DeLeteList(DeleteListRequest request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            foreach (var id in request.Ids)
            {
                var entity = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().GetByIdAsync(id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.Now;
                    entity.DeletedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

                    await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().UpdateAsync(entity);
                }

                await unitOfWork.SaveChangesAsync();
            }
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