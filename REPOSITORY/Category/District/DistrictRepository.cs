using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DAL.Entities;
using Dapper;
using DTO.Base;
using DTO.Category.District.Models;
using REPOSITORY.Common;

namespace REPOSITORY.Category.District;

[RegisterClassAsTransient]
public class DistrictRepository(IUnitOfWork unitOfWork, IMapper mapper) : IDistrictRepository
{
    public async Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iPageIndex", request.Offset / request.Limit, DbType.Int32);
        parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
        parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

        var result = await unitOfWork.GetRepository<DistrictModel>().ExecWithStoreProcedure("sp_Category_District_GetListPaging", parameters);

        var totalRow = parameters.Get<long>("@oTotalRow");
        var responseData = new GetListPagingResponse
        {
            PageIndex = request.Offset,
            Data = result,
            TotalRow = Convert.ToInt32(totalRow)
        };
            
        return responseData;
    }
    
    public async Task<DistrictModel> GetById(GetByIdRequest request)
    {
        var data = await unitOfWork.GetRepository<Districts>().GetByIdAsync(request.Id);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<DistrictModel>(data);
        return result;
    }
    public List<ComboboxModel> GetAllForCombobox(GetAllRequest request)
    {
        var result = unitOfWork.GetRepository<Districts>()
            .GetAll(x => !string.IsNullOrEmpty(x.Code))
            .OrderBy(x => x.Name)
            .ToList();

        var response = result.Select(x => new ComboboxModel
        {
            Text = x.Name,
            Value = x.Code.ToString()
        }).OrderBy(x => x.Sort).ToList();

        return response;
    }

    
    

    
}