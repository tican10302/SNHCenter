using DTO.Base;
using DTO.Management.LessonTemplate.Dtos;
using DTO.Management.LessonTemplate.Models;

namespace REPOSITORY.Management.LessonTemplate;

public interface ILessonTemplateRepository
{
    Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
    List<LessonTemplateModel> GetAll(GetListLessonTemplateRequest request);
    Task<LessonTemplateDto> GetByPost(GetByIdRequest request);
    Task<LessonTemplateModel> GetById(GetByIdRequest request);
    Task<bool> InsertList(List<LessonTemplateDto> request);
    Task<bool> UpdateList(List<LessonTemplateDto> request);
    Task<bool> DeLeteList(DeleteListRequest request);
}