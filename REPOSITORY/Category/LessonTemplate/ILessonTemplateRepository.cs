using DTO.Base;
using DTO.Category.LessonTemplate.Dtos;
using DTO.Category.LessonTemplate.Models;

namespace REPOSITORY.Category.LessonTemplate;

public interface ILessonTemplateRepository
{
    Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
    Task<BaseResponse<LessonTemplateModel>> GetById(GetByIdRequest request);
    Task<BaseResponse<LessonTemplateDto>> GetByPost(GetByIdRequest request);
    Task<BaseResponse<LessonTemplateModel>> Insert(LessonTemplateDto request);
    Task<BaseResponse<LessonTemplateModel>> Update(LessonTemplateDto request);
    Task<BaseResponse<string>> DeLeteList(DeleteListRequest request);
}