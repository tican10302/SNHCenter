using AutoMapper;
using Dapper;
using DTO.Base;
using DTO.System.Role.Dtos;
using DTO.System.Role.Models;
using Microsoft.AspNetCore.Http;
using REPOSITORY.Common;
using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;

namespace REPOSITORY.System.Role
{
    [RegisterClassAsTransient]
    public class RoleRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IRoleRepository
    {
        public async Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iTextSearch", request.Search, DbType.String);
            parameters.Add("@iPageIndex", request.Offset, DbType.Int32);
            parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
            parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

            var result = await unitOfWork.GetRepository<RoleModel>().ExecWithStoreProcedure("sp_Sys_Role_GetListPaging", parameters);

            var totalRow = parameters.Get<long>("@oTotalRow");
            var responseData = new GetListPagingResponse
            {
                PageIndex = request.Offset,
                Data = result,
                TotalRow = Convert.ToInt32(totalRow)
            };

            return responseData;
        }

        public async Task<RoleModel> GetById(GetByIdRequest request)
        {
            var data = await unitOfWork.GetRepository<DAL.Entities.Role>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
            }

            var result = mapper.Map<RoleModel>(data);
            return result;
        }

        public async Task<RoleDto> GetByPost(GetByIdRequest request)
        {
            var result = new RoleDto();
            var data = await unitOfWork.GetRepository<DAL.Entities.Role>().GetByIdAsync(request.Id);
            if (data == null)
            {
                result.Id = Guid.NewGuid();
                result.IsEdit = false;
            }
            else
            {
                result = mapper.Map<RoleDto>(data);
                result.IsEdit = true;
            }

            return result;
        }

        public async Task<bool> Insert(RoleDto request)
        {
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();

                var checkData = await unitOfWork.GetRepository<DAL.Entities.Role>().Find(x =>
                !x.IsDeleted &&
                    x.Name == request.Name);
                if (checkData != null)
                {
                    throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
                }

                var entity = mapper.Map<DAL.Entities.Role>(request);
                entity.CreatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
                entity.UpdatedAt = DateTime.Now;

                await unitOfWork.GetRepository<DAL.Entities.Role>().AddAsync(entity);

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

        public async Task<bool> Update(RoleDto request)
        {
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();

                var checkData = await unitOfWork.GetRepository<DAL.Entities.Role>().Find(x =>
                    !x.IsDeleted &&
                    x.Id != request.Id &&
                    x.Name == request.Name);
                if (checkData != null)
                {
                    throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
                }

                var data = await unitOfWork.GetRepository<DAL.Entities.Role>().GetByIdAsync(request.Id);
                if (data == null)
                {
                    throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
                }
                var entity = mapper.Map(request, data);

                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

                await unitOfWork.GetRepository<DAL.Entities.Role>().UpdateAsync(entity);

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


        public async Task<bool> DeleteList(DeleteListRequest request)
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            foreach (var id in request.Ids)
            {
                var entity = await unitOfWork.GetRepository<DAL.Entities.Role>().GetByIdAsync(id);

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
                entity.DeletedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

                await unitOfWork.GetRepository<DAL.Entities.Role>().UpdateAsync(entity);

                await unitOfWork.SaveChangesAsync();
            }
            await unitOfWork.CommitAsync();
            return true;
        }

        public List<ComboboxModel> GetAllForCombobox()
        {
            var result = unitOfWork.GetRepository<DAL.Entities.Role>()
                .GetAll(x => !x.IsDeleted && x.IsActived)
                .OrderBy(x => x.Name)
                .ToList();

            var response = result.Select(x => new ComboboxModel
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).OrderBy(x => x.Sort).ToList();
            
            return response;
        }

        //GET LIST ROLE PERMISSION
        public async Task<List<Role_PermissionModel>> GetListRolePermission(GetRole_PermissionDto request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iGroupId", request.GroupId);
            parameters.Add("@iRoleId", request.RoleId);

            var data = await unitOfWork.GetRepository<Role_PermissionModel>().ExecWithStoreProcedure("sp_Sys_Account_GetPermission", parameters);

            return data;
        }

        //POST ROLE PERMISSION
        public async Task<Role_PermissionModel> PostRolePermission(Role_PermissionDto request)
        {
            Role_PermissionModel response;
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();
                var resultUpdate = await unitOfWork.GetRepository<DAL.Entities.Permission>().Find(x => x.Id == request.Id);
                if (resultUpdate == null)
                {
                    request.Id = Guid.NewGuid();
                    var add = mapper.Map<DAL.Entities.Permission>(request);
                    await unitOfWork.GetRepository<DAL.Entities.Permission>().AddAsync(add);
                    response = mapper.Map<Role_PermissionModel>(add);
                }
                else
                {
                    resultUpdate.IsEdit = request.IsEdit;
                    resultUpdate.IsApprove = request.IsApprove;
                    resultUpdate.IsAdd = request.IsAdd;
                    resultUpdate.IsStatistic = request.IsStatistic;
                    resultUpdate.IsView = request.IsView;
                    resultUpdate.IsDelete = request.IsDelete;
                    await unitOfWork.GetRepository<DAL.Entities.Permission>().UpdateAsync(resultUpdate);
                    response = mapper.Map<Role_PermissionModel>(resultUpdate);
                }
                await unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
            return response;
        }
    }
}
