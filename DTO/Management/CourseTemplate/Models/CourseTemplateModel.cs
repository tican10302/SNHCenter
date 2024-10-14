using DTO.Base;

namespace DTO.Management.CourseTemplate.Models;

public class CourseTemplateModel : ModelBase
{
    public Guid? LevelId { get; set; }
    public string? Level { get; set; }
}