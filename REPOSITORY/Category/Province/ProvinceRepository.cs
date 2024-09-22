﻿using System.Data;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DAL.Entities;
using Dapper;
using DTO.Base;
using DTO.Category.Province.Models;
using DTO.Category.Province.Dtos;
using REPOSITORY.Common;
using Microsoft.AspNetCore.Http;


namespace REPOSITORY.Category;

[RegisterClassAsTransient]
public class ProvinceRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IProvinceRepository
{
    public async Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request)
    {
        var response = new BaseResponse<List<ComboboxModel>>();

        try
        {
            var result = unitOfWork.GetRepository<DAL.Entities.Provinces>()
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

    public async Task<BaseResponse<ProvinceModel>> GetById(GetByIdRequest request)
    {
        var response = new BaseResponse<ProvinceModel>();

        try
        {
            var data = await unitOfWork.GetRepository<Provinces>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }

            var result = mapper.Map<ProvinceModel>(data);
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

    public Task<BaseResponse<ProvinceDto>> GetByPost(GetByIdRequest request)
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

            var result = await unitOfWork.GetRepository<ProvinceModel>().ExecWithStoreProcedure("sp_Category_Province_GetListPaging", parameters);

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