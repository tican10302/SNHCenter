using DTO.Base;
using DTO.Management.Parent.Models;
using DTO.Management.Parent.Dtos;

namespace REPOSITORY.Management.Parent
{
    public interface IParentRepository
    {
        Task<GetListPagingResponse> GetListPaging(ParentGetListDto request);
        Task<ParentDto> GetByPost(GetByIdRequest request);
        Task<ParentModel> GetById(GetByIdRequest request);
        Task<bool> Insert(ParentDto request);
        Task<bool> Update(ParentDto request);
        Task<bool> DeLeteList(DeleteListRequest request);
        List<ComboboxModel> GetAllForCombobox();
    }
}
