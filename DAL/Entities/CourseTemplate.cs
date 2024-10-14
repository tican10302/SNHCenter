using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class CourseTemplate : EntitiesBase
{
    [MaxLength(200)]
    public required Level Level { get; set; }
    public Guid LevelId { get; set; }
}