using DTO.Base;
using DTO.Category.Ward.Dtos;
using DTO.Category.Ward.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Category.Ward
{
    public interface IWardRepository
    {
        Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
        Task<BaseResponse<WardDto>> GetByPost(GetByIdRequest request);
        Task<BaseResponse<WardModel>> GetById(GetByIdRequest request);
        Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request);
    }
}
