using DTO.Base;
using DTO.Category.Shift.Models;
using DTO.Category.Shift.Dtos;

namespace REPOSITORY.Category;

public interface IShiftRepository
{
    Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
    Task<BaseResponse<ShiftDto>> GetByPost(GetByIdRequest request);
    Task<BaseResponse<ShiftModel>> GetById(GetByIdRequest request);
    Task<BaseResponse<ShiftModel>> Insert(ShiftDto request);
    Task<BaseResponse<ShiftModel>> Update(ShiftDto request);
    Task<BaseResponse<string>> DeLeteList(DeleteListRequest request);
    Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request);
}