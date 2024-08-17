using DTO.Base;
using DTO.Category.Shift.Requests;
using DTO.Category.Shift.Dtos;

namespace REPOSITORY.Category;

public interface IShiftRepository
{
    Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
    Task<BaseResponse<ShiftDto>> GetByPost(Guid id);
    Task<BaseResponse<ShiftModel>> GetById(Guid id);
    Task<BaseResponse<ShiftModel>> Add(ShiftDto request);
    Task<BaseResponse<string>> Update(ShiftDto request);
    Task<BaseResponse<string>> DeLeteList(DeleteListRequest request);
    // Task<BaseResponse<List<MODELCombobox>>> GetAllForCombobox(GetAllRequest request);
}