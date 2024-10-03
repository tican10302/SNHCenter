using DTO.Base;
using DTO.Category.Ward.Models;

namespace REPOSITORY.Category.Ward
{
    public interface IWardRepository
    {
        Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        Task<WardModel> GetById(string request);
        List<ComboboxModel> GetAllForCombobox(GetAllRequest request);
    }
}
