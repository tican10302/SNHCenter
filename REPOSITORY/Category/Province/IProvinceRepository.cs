using DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using DTO.Base;
using DTO.Category.Province.Models;
using DTO.Category.Province.Dtos;

namespace REPOSITORY.Category
{
    public interface IProvinceRepository
    {
        Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
        Task<BaseResponse<ProvinceDto>> GetByPost(GetByIdRequest request);
        Task<BaseResponse<ProvinceModel>> GetById(GetByIdRequest request);
        Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request);
    }
}
