using DTO.Base;
using DTO.Category.Shift;

namespace REPOSITORY.Category;

public interface IShiftRepository
{
    Task<BaseResponse<GetListPagingResponse>> GetListPagingAsync(GetListPagingRequest request);
    Task<BaseResponse<List<ShiftModel>>> GetAllAsync();
    Task<BaseResponse<ShiftModel>> GetByIdAsync(Guid id);
    Task<BaseResponse<ShiftModel>> AddAsync(ShiftDto request);
    Task<BaseResponse<string>> UpdateAsync(ShiftDto request);
    Task<BaseResponse<string>> DeLeteListAsync(DeleteListRequest request);
    // Task<BaseResponse<List<MODELCombobox>>> GetAllForCombobox(GetAllRequest request);
}