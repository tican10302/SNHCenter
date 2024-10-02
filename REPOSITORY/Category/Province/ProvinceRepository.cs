using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DAL.Data;
using Dapper;
using DTO.Base;
using DTO.Category.Province.Models;
using REPOSITORY.Common;


namespace REPOSITORY.Category.Province;

[RegisterClassAsTransient]
public class ProvinceRepository(IUnitOfWork unitOfWork, IMapper mapper, DataContext context) : IProvinceRepository
{
    public List<ComboboxModel> GetAllForCombobox(GetAllRequest request)
    {
        var result = unitOfWork.GetRepository<DAL.Entities.Provinces>()
            .GetAll(x => !string.IsNullOrEmpty(x.Code))
            .OrderBy(x => x.Name)
            .ToList();

        List<ComboboxModel> response = result.Select(x => new ComboboxModel
        {
            Text = x.Name,
            Value = x.Code.ToString()
        }).OrderBy(x => x.Sort).ToList();

        return response;
    }

    public async Task<ProvinceModel> GetById(string request)
    {
        var data = await context.Provinces.FindAsync(request);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<ProvinceModel>(data);
        return result;
    }

    public async Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iPageIndex", request.Offset / request.Limit, DbType.Int32);
        parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
        parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

        var result = await unitOfWork.GetRepository<ProvinceModel>().ExecWithStoreProcedure("sp_Category_Province_GetListPaging", parameters);

        var totalRow = parameters.Get<long>("@oTotalRow");
        var response = new GetListPagingResponse()
        {
            PageIndex = request.Offset,
            Data = result,
            TotalRow = Convert.ToInt32(totalRow)
        };

        return response;
    }
}