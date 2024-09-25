using DTO.Base;
using DTO.Category.LessonTemplate.Dtos;
using DTO.Category.LessonTemplate.Models;

namespace REPOSITORY.Category.LessonTemplate;

public interface ILessonTemplateRepository
{
    Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
    Task<LessonTemplateModel> GetById(GetByIdRequest request);
    Task<LessonTemplateDto> GetByPost(GetByIdRequest request);
    Task<bool> Insert(LessonTemplateDto request);
    Task<bool> Update(LessonTemplateDto request);
    Task<bool> DeleteList(DeleteListRequest request);
}