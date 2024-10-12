using DTO.Base;

namespace DTO.Management.CourseTemplate.Models;

public class CourseTemplateModel : ModelBase
{
    public string? Name { get; set; }
    public Guid? LevelId { get; set; }
    public string? Level { get; set; }
}