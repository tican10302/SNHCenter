using AutoMapper;
using DAL.Entities;
using Dapper;
using DTO.Base;
using DTO.Category.DAL.Entities.Role.Dtos;
using DTO.Category.DAL.Entities.Role.Models;
using DTO.System.Role.Dtos;
using DTO.System.Role.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using REPOSITORY.Common;
using System.Data;

namespace REPOSITORY.System.Role
{
    public class RoleRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IRoleRepository
    {
        public async Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request)
        {
            var response = new BaseResponse<GetListPagingResponse>();

            try
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

                response.Data = responseData;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<RoleModel>> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<RoleModel>();

            try
            {
                var data = await unitOfWork.GetRepository<DAL.Entities.Role>().GetByIdAsync(request.Id);
                if (data == null)
                {
                    throw new Exception("Not data found");
                }

                var result = mapper.Map<RoleModel>(data);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<RoleDto>> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<RoleDto>();

            try
            {
                var result = new RoleDto();
                var data = await unitOfWork.GetRepository<DAL.Entities.Role>().GetByIdAsync(request.Id);
                if (result == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = mapper.Map<RoleDto>(data);
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

        public async Task<BaseResponse<RoleModel>> Insert(RoleDto request)
        {
            var response = new BaseResponse<RoleModel>();
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();

                var checkData = await unitOfWork.GetRepository<DAL.Entities.Role>().Find(x =>
                !x.IsDeleted &&
                    x.Name == request.Name);
                if (checkData != null)
                {
                    throw new Exception("Data already exists");
                }

                var entity = mapper.Map<DAL.Entities.Role>(request);
                entity.CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
                entity.CreatedAt = DateTime.Now; ;
                entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name;
                entity.UpdatedAt = DateTime.Now;

                var result = await unitOfWork.GetRepository<DAL.Entities.Role>().AddAsync(entity);

                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitAsync();

                response.Data = mapper.Map<RoleModel>(result);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<RoleModel>> Update(RoleDto request)
        {
            var response = new BaseResponse<RoleModel>();
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();

                var checkData = await unitOfWork.GetRepository<DAL.Entities.Role>().Find(x =>
                    !x.IsDeleted &&
                    x.Id != request.Id &&
                    x.Name == request.Name);
                if (checkData != null)
                {
                    throw new Exception("Data already exists");
                }

                var data = await unitOfWork.GetRepository<DAL.Entities.Role>().GetByIdAsync(request.Id);
                if (data == null)
                {
                    throw new Exception("Not data found");
                }
                var entity = mapper.Map(request, data);

                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = httpContextAccessor.HttpContext.User.Identity.Name;

                await unitOfWork.GetRepository<DAL.Entities.Role>().UpdateAsync(entity);

                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitAsync();

                response.Data = mapper.Map<RoleModel>(entity);
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
                    var entity = await unitOfWork.GetRepository<DAL.Entities.Role>().GetByIdAsync(id);

                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.Now;
                    entity.DeletedBy = httpContextAccessor.HttpContext.User.Identity.Name;

                    await unitOfWork.GetRepository<DAL.Entities.Role>().UpdateAsync(entity);

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
                var result = unitOfWork.GetRepository<DAL.Entities.Role>()
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

        //GET LIST ROLE PERMISSION
        public async Task<BaseResponse<List<Role_PermissionModel>>> GetListRolePermission(GetRole_PermissionDto request)
        {
            var response = new BaseResponse<List<Role_PermissionModel>>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@iGroupId", request.GroupId);
                parameters.Add("@iRoleId", request.RoleId);

                response.Data = await unitOfWork.GetRepository<Role_PermissionModel>().ExecWithStoreProcedure("sp_Sys_Account_GetPermission", parameters);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //POST ROLE PERMISSION
        public async Task<BaseResponse<Role_PermissionModel>> PostRolePermission(Role_PermissionDto request)
        {
            var response = new BaseResponse<Role_PermissionModel>();
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();
                var resultUpdate = await unitOfWork.GetRepository<DAL.Entities.Permission>().Find(x => x.Id == request.Id);
                if (resultUpdate == null)
                {
                    request.Id = Guid.NewGuid();
                    var add = mapper.Map<DAL.Entities.Permission>(request);
                    unitOfWork.GetRepository<DAL.Entities.Permission>().AddAsync(add);
                    unitOfWork.CommitAsync();
                    response.Data = mapper.Map<Role_PermissionModel>(add);
                }
                else
                {
                    resultUpdate.IsEdit = request.IsEdit;
                    resultUpdate.IsApprove = request.IsApprove;
                    resultUpdate.IsAdd = request.IsAdd;
                    resultUpdate.IsStatistic = request.IsStatistic;
                    resultUpdate.IsView = request.IsView;
                    resultUpdate.IsDelete = request.IsDelete;
                    unitOfWork.GetRepository<DAL.Entities.Permission>().UpdateAsync(resultUpdate);
                    unitOfWork.CommitAsync();
                    response.Data = mapper.Map<Role_PermissionModel>(resultUpdate);
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
