namespace DAL.Entities;

public class CourseTemplate : EntitiesBase
{
    public required Level Level { get; set; }
    public Guid LevelId { get; set; }
}