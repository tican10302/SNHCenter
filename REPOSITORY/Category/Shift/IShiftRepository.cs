using DTO.Base;
using DTO.Category.Shift.Models;
using DTO.Category.Shift.Dtos;

namespace REPOSITORY.Category.Shift
{
    public interface IShiftRepository
    {
        Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        Task<ShiftDto> GetByPost(GetByIdRequest request);
        Task<ShiftModel> GetById(GetByIdRequest request);
        Task<bool> Insert(ShiftDto request);
        Task<bool> Update(ShiftDto request);
        Task<bool> DeLeteList(DeleteListRequest request);
        List<ComboboxModel> GetAllForCombobox();
    }
}
