using DTO.Base;

namespace DTO.Management.LessonTemplate.Dtos;

public class GetListLessonTemplateRequest : GetListPagingRequest
{
    public Guid? CourseTemplateId { get; set; } = Guid.Empty;
}