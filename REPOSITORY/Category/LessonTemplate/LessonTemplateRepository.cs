using System.Data;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DAL.Entities;
using Dapper;
using DTO.Base;
using DTO.Category.LessonTemplate.Dtos;
using DTO.Category.LessonTemplate.Models;
using Microsoft.AspNetCore.Http;
using REPOSITORY.Common;

namespace REPOSITORY.Category.LessonTemplate;

[RegisterClassAsTransient]
public class LessonTemplateRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : ILessonTemplateRepository
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

            var result = await unitOfWork.GetRepository<LessonTemplateModel>().ExecWithStoreProcedure("sp_Category_LessonTemplate_GetListPaging", parameters);

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

    public async Task<BaseResponse<LessonTemplateModel>> GetById(GetByIdRequest request)
    {
        var response = new BaseResponse<LessonTemplateModel>();

        try
        {
            var data = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }

            var result = mapper.Map<LessonTemplateModel>(data);
            response.Data = result;
        }
        catch(Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<LessonTemplateDto>> GetByPost(GetByIdRequest request)
    {
        var response = new BaseResponse<LessonTemplateDto>();

        try
        {
            var result = new LessonTemplateDto();
            var data = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().GetByIdAsync(request.Id);
            if (result == null)
            {
                result.Id = Guid.NewGuid();
                result.IsEdit = false;
            }
            else
            {
                result = mapper.Map<LessonTemplateDto>(data);
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

    public async Task<BaseResponse<LessonTemplateModel>> Insert(LessonTemplateDto request)
    {
        var response = new BaseResponse<LessonTemplateModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().Find(x => 
                !x.IsDeleted &&
                (x.CourseTemplateId == request.CourseTemplateId
                && x.LessonNo == request.LessonNo));
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }
            
            // if(request.CourseTemplateId == Guid.Empty)
            // {
            //     CourseTemplate courseTemplate = new CourseTemplate()
            //     {
            //         Id = Guid.NewGuid(),
            //         LevelId = request.LevelId??Guid.Empty,
            //     };
            //     await unitOfWork
            // }
            
            var entity = mapper.Map<DAL.Entities.LessonTemplate>(request);
            entity.CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            entity.CreatedAt = DateTime.Now;;
            entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            entity.UpdatedAt = DateTime.Now;
            
            var result = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().AddAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            
            response.Data = mapper.Map<LessonTemplateModel>(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<LessonTemplateModel>> Update(LessonTemplateDto request)
    {
        var response = new BaseResponse<LessonTemplateModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().Find(x =>
                !x.IsDeleted &&
                x.Id != request.Id &&
                (x.CourseTemplateId == request.CourseTemplateId
                 && x.LessonNo == request.LessonNo));
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }

            var data = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }
            var entity = mapper.Map(request, data);

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            
            await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().UpdateAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            response.Data = mapper.Map<LessonTemplateModel>(entity);
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
                var entity = await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().GetByIdAsync(id);

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
                entity.DeletedBy = httpContextAccessor.HttpContext.User.Identity.Name;
            
                await unitOfWork.GetRepository<DAL.Entities.LessonTemplate>().UpdateAsync(entity);

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