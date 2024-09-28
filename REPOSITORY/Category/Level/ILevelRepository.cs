using DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using DTO.Base;
using DTO.Category.Level.Models;
using DTO.Category.Level.Dtos;

namespace REPOSITORY.Category
{
    public interface ILevelRepository
    {
        Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
        Task<BaseResponse<LevelDto>> GetByPost(GetByIdRequest request);
        Task<BaseResponse<LevelModel>> GetById(GetByIdRequest request);
        Task<BaseResponse<LevelModel>> Insert(LevelDto request);
        Task<BaseResponse<LevelModel>> Update(LevelDto request);
        Task<BaseResponse<string>> DeLeteList(DeleteListRequest request);
        Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request);
    }
}
