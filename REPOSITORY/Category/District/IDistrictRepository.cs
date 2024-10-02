using DTO.Base;
using DTO.Category.District.Models;

namespace REPOSITORY.Category.District
{
    public interface IDistrictRepository
    {
        Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        Task<DistrictModel> GetById(GetByIdRequest request);
        List<ComboboxModel> GetAllForCombobox(GetAllRequest request);
    }
}
