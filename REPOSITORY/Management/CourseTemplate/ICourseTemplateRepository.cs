using DTO.Base;
using DTO.Management.CourseTemplate.Dtos;
using DTO.Management.CourseTemplate.Models;

namespace REPOSITORY.Management.CourseTemplate;

public interface ICourseTemplateRepository
{
    Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
    Task<CourseTemplateDto> GetByPost(GetByIdRequest request);
    Task<CourseTemplateModel> GetById(GetByIdRequest request);
    Task<bool> Insert(CourseTemplateDto request);
    Task<bool> Update(CourseTemplateDto request);
    Task<bool> DeLeteList(DeleteListRequest request);
}