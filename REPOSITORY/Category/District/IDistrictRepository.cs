using DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using DTO.Base;
using DTO.Category.District.Models;
using DTO.Category.District.Dtos;

namespace REPOSITORY.Category
{
    public interface IDistrictRepository
    {
        Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
        Task<BaseResponse<DistrictDto>> GetByPost(GetByIdRequest request);
        Task<BaseResponse<DistrictModel>> GetById(GetByIdRequest request);
        Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request);
    }
}
