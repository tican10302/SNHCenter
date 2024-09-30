using DTO.Base;
using DTO.Category.Program.Models;
using DTO.Category.Program.Dtos;

namespace REPOSITORY.Category.Program
{
    public interface IProgramRepository
    {
        Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        Task<ProgramDto> GetByPost(GetByIdRequest request);
        Task<ProgramModel> GetById(GetByIdRequest request);
        Task<bool> Insert(ProgramDto request);
        Task<bool> Update(ProgramDto request);
        Task<bool> DeLeteList(DeleteListRequest request);
        List<ComboboxModel> GetAllForCombobox();
    }
}
