﻿using DTO.Base;
using DTO.Category.Shift.Dtos;
using DTO.Category.Shift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DTO.Base;
using DTO.Category.Program.Models;
using DTO.Category.Program.Dtos;

namespace REPOSITORY.Category
{
    public interface IsProgramRepository
    {
        Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
        Task<BaseResponse<ProgramDto>> GetByPost(GetByIdRequest request);
        Task<BaseResponse<ProgramModel>> GetById(GetByIdRequest request);
        Task<BaseResponse<ProgramModel>> Insert(ProgramDto request);
        Task<BaseResponse<ProgramModel>> Update(ProgramDto request);
        Task<BaseResponse<string>> DeLeteList(DeleteListRequest request);
        Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request);
    }
}
