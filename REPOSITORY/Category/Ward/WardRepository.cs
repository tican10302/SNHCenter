using System.Data;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DAL.Entities;
using Dapper;
using DTO.Base;
using DTO.Category.Ward.Models;
using DTO.Category.Ward.Dtos;
using REPOSITORY.Common;
using Microsoft.AspNetCore.Http;
using DTO.Category.Ward.Dtos;
using DTO.Category.Ward.Models;

namespace REPOSITORY.Category.Ward
{
    [RegisterClassAsTransient]
    public class WardRepositorỵ̣̣(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IWardRepository
    {
        public async Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request)
        {
            var response = new BaseResponse<List<ComboboxModel>>();

            try
            {
                var result = unitOfWork.GetRepository<DAL.Entities.Wards>()
                    .GetAll(x => !string.IsNullOrEmpty(x.Code))
                    .OrderBy(x => x.Name)
                    .ToList();

                response.Data = result.Select(x => new ComboboxModel
                {
                    Text = x.Name,
                    Value = x.Code.ToString()
                }).OrderBy(x => x.Sort).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<WardModel>> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<WardModel>();

            try
            {
                var data = await unitOfWork.GetRepository<Wards>().GetByIdAsync(request.Id);
                if (data == null)
                {
                    throw new Exception("Not data found");
                }

                var result = mapper.Map<WardModel>(data);
                //result.SelectDays = result.Days.Split(", ").ToList();
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public Task<BaseResponse<WardDto>> GetByPost(GetByIdRequest request)
        {
            throw new NotImplementedException();
        }

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

                var result = await unitOfWork.GetRepository<WardModel>().ExecWithStoreProcedure("sp_Category_Ward_GetListPaging", parameters);

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
    }
}