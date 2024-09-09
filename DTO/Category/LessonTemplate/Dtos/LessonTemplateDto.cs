using DTO.Base;

namespace DTO.Category.LessonTemplate.Dtos;

public class LessonTemplateDto : DtoBase
{
    public int? LessonNo { get; set; }
    public int? HourDone { get; set; }
    public string? CourseBookPage { get; set; }
    public string? LessonAim { get; set; }
    public string? AdditionalInformation { get; set; }
    public Guid? LevelId { get; set; }
    public Guid? CourseTemplateId { get; set; }
}