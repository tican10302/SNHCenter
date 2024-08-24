using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Dapper;
using DTO.Base;
using DTO.System.Account.Models;
using DTO.System.GroupPermission.Dtos;
using REPOSITORY.Common;

namespace REPOSITORY.System.GroupPermission;

[RegisterClassAsTransient]
public class GroupPermissionRepository(IUnitOfWork unitOfWork, IMapper mapper) : IGroupPermissionRepository
{
    public async Task<BaseResponse<List<GroupPermissionModel>>> GetList(GetAllRequest request)
    {
        var response = new BaseResponse<List<GroupPermissionModel>>();

        try
        {
            var result = unitOfWork.GetRepository<DAL.Entities.GroupPermission>()
                .GetAll(x => x.IsActived || !x.IsActived)
                .OrderBy(x => x.Sort)
                .ToList();

            response.Data = mapper.Map<List<GroupPermissionModel>>(result);
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<GroupPermissionModel>> GetById(GetByIdRequest request)
    {
        var response = new BaseResponse<GroupPermissionModel>();

        try
        {
            var data = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }

            var result = mapper.Map<GroupPermissionModel>(data);
            response.Data = result;
        }
        catch(Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<GroupPermissionDto>> GetByPost(GetByIdRequest request)
    {
        var response = new BaseResponse<GroupPermissionDto>();

        try
        {
            var result = new GroupPermissionDto();
            var data = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().GetByIdAsync(request.Id);
            if (result == null)
            {
                result.Id = Guid.NewGuid();
                result.IsEdit = false;
            }
            else
            {
                result = mapper.Map<GroupPermissionDto>(data);
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

    public async Task<BaseResponse<GroupPermissionModel>> Insert(GroupPermissionDto request)
    {
        var response = new BaseResponse<GroupPermissionModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().Find(x => 
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }
            
            var entity = mapper.Map<DAL.Entities.GroupPermission>(request);
            var result = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().AddAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            
            response.Data = mapper.Map<GroupPermissionModel>(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<GroupPermissionModel>> Update(GroupPermissionDto request)
    {
        var response = new BaseResponse<GroupPermissionModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().Find(x =>
                x.Id != request.Id &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }

            var data = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }
            var entity = mapper.Map(request, data);
            await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().UpdateAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            response.Data = mapper.Map<GroupPermissionModel>(entity);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<List<GroupPermissionModel>>> GetAll(GetAllRequest request)
    {
        var response = new BaseResponse<List<GroupPermissionModel>>();

        try
        {
            var result = unitOfWork.GetRepository<DAL.Entities.GroupPermission>()
                .GetAll(x => x.IsActived)
                .OrderBy(x => x.Sort)
                .ToList();

            response.Data = mapper.Map<List<GroupPermissionModel>>(result);
        }
        catch (Exception ex)
        {
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
            var result = unitOfWork.GetRepository<DAL.Entities.GroupPermission>()
                .GetAll(x => x.IsActived)
                .OrderBy(x => x.Sort)
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