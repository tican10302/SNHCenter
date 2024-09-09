using DTO.Base;

namespace DTO.Training.Course.Models;

public class CourseModel : ModelBase
{
    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Center { get; set; }
    public string? Room { get; set; }
    public Guid? ShiftId { get; set; }
    public Guid? LevelId { get; set; }
}