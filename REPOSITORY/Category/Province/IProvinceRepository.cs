using DTO.Base;
using DTO.Category.Province.Models;

namespace REPOSITORY.Category.Province
{
    public interface IProvinceRepository
    {
        Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        Task<ProvinceModel> GetById(string request);
        List<ComboboxModel> GetAllForCombobox(GetAllRequest request);
    }
}
