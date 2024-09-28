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
        Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        Task<ProvinceModel> GetById(GetByIdRequest request);
        List<ComboboxModel> GetAllForCombobox(GetAllRequest request);
    }
}
